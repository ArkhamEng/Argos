using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Support;
using Argos.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Argos.Controllers
{
    public partial class CatalogController
    {
        #region Client Methods
        public ActionResult Products()
        {
            var model = new List<List<Product>>();

            ViewBag.Categories      = db.Categories.ToSelectList();
            ViewBag.SubCategories  = new List<SubCategory>().ToSelectList();
            ViewBag.CarMakes  = db.CarMakes.ToSelectList();
            ViewBag.CarModels = new List<CarModel>().ToSelectList();
            ViewBag.CarYears  = new List<CarYear>().ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchProducts(ProductFilterViewModel filter)
        {
            var model = LookFor(filter);
            return PartialView("_ProductList", model);
        }

        private List<List<Product>> LookFor(ProductFilterViewModel filter)
        {
            //obtengo la sucursa en sesión
            var branchId = User.Identity.GetBranchId();

            //configuro variables auxiliares
            string[] arr = new List<string>().ToArray();
            string code = null;

            //si el filtro de texto tiene datos
            if (filter.Text != null && filter.Text != string.Empty)
            {
                //divido el texto por palabras y lo convierto en un array
                arr = filter.Text.Trim().Split(' ');

                //si solo hay  una palabra se considera como un posible código
                if (arr.Length == Cons.One)
                    code = arr.FirstOrDefault();
            }

            List<Product> products = new List<Product>();

            //se realiza la busqueda con el "posible código"
            if (code != null)
            {
                products = (from p in db.Products.Include(p => p.ProductImages).Include(p => p.Stocks).
                            Include(p => p.Compatibilities).Include(p => p.Compatibilities.Select(c => c.CarYear)).
                            Include(p => p.Compatibilities.Select(c => c.CarYear.CarModel))
                            where (p.Code == code)
                            select p).ToList();
            }

            // si no se encuentra un código que coincida, se realiza una búsqueda con todos lo filtros
            //buscando coincidencias con todas las palabras del array entre código, descripción y marca
            if (products.Count == Cons.Zero)
            {
                products = (from p in db.Products.Include(p => p.ProductImages).Include(p => p.Stocks).
                            Include(p => p.SubCategory).Include(p => p.SubCategory.Category).Include(p => p.Compatibilities).
                            Include(p => p.Compatibilities.Select(c => c.CarYear))
                            where (filter.CategoryId == null || p.SubCategory.CategoryId == filter.CategoryId)
                            && (filter.CategoryId == null || p.SubCategoryId == filter.SubCategoryId)
                            && (filter.Text == null || filter.Text == string.Empty ||
                                arr.All(s => (p.Code + " " + p.Description + " " + p.TradeMark).Contains(s)))
                            //si alunos de los filtros de auto es requerido, se realiza el filtrado
                            && (filter.CarYearId == null || p.Compatibilities.Where(c => c.CarYearId == filter.CarYearId).ToList().Count > Cons.Zero)
                            && (filter.CarModelId == null || p.Compatibilities.Where(c => c.CarYear.CarModelId == filter.CarModelId).ToList().Count > Cons.Zero)
                            && (filter.CarMakeId == null || p.Compatibilities.Where(c => c.CarYear.CarModel.CarMakeId == filter.CarMakeId).ToList().Count > Cons.Zero)

                            select p).OrderBy(s => s.Description).Take(Cons.MaxSearchRows).ToList();
            }

            //lleno los datos de inventario de la sucursal activa en la sesión
            products.ForEach(p =>
            {
                var bp = p.Stocks.FirstOrDefault(b => b.BranchId == branchId);

                p.Available = bp != null ? bp.Available : Cons.Zero;
                p.Row = bp != null ? bp.Row : string.Empty;
                p.Ledge = bp != null ? bp.Ledge : string.Empty;
            });

            if (filter.IsGrid)
                return OrderAsGrid(products);
            else
            {
                List<List<Product>> productList = new List<List<Product>>();

                products.ForEach(p =>
                {
                    List<Product> pl = new List<Product>();
                    pl.Add(p);
                    productList.Add(pl);
                });

                return productList;
            }
        }

        private List<List<Product>> OrderAsGrid(List<Product> products)
        {
            List<List<Product>> prodMod = new List<List<Product>>();

            bool newRow = true;
            List<Product> list = null;

            for (int i = Cons.Zero; i < products.Count; i++)
            {
                if (newRow)
                {
                    list = new List<Product>();
                    list.Add(products[i]);

                    if (i == products.Count - Cons.One)
                        prodMod.Add(list);

                    newRow = false;
                }
                else
                {
                    list.Add(products[i]);
                    prodMod.Add(list);
                    newRow = true;
                }
            }

            return prodMod;
        }

      
        [HttpPost]
        public ActionResult BeginAddProduct()
        {
            var model = new Product();

            ViewBag.Categories = db.States.ToSelectList();
            ViewBag.SubCategories = new List<City>().ToSelectList();
            ViewBag.MeassureUnits = db.MeasureUnits.ToSelectList();

            return PartialView("_ProductEdit", model);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            try
            {
                product.InsDate = DateTime.Now.ToLocal();
                product.UpdDate = DateTime.Now.ToLocal();
                product.UpdUser = User.Identity.Name;
                product.InsUser = User.Identity.Name;
                product.IsActive = true;

                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseWarning,
                    Header = "Error al guardar el productto",
                    Body = "Ocurrio un error al agregar el producto " + ex.Message
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del producto guardados",
                Body = string.Format("El product {0} fue agregado al catálogo", product.Description),
                Id = product.ProductId
            });
        }
        [HttpPost]
        public ActionResult BeginUpdateProduct(int id)
        {
            try
            {
                var model = db.Products.Include(p => p.ProductImages).
                    FirstOrDefault(p => p.ProductId == id);

                if (model != null)
                {
                    ViewBag.Categories = new SelectList(db.Categories, nameof(State.StateId), nameof(State.Name),
                                                            model.SubCategory.Category.CategoryId);

                    ViewBag.SubCategories = db.SubCategories.Where(sc => sc.CategoryId == model.SubCategory.CategoryId).ToSelectList();

                    return PartialView("_ProductEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "Producto inexistente!",
                        Body = string.Format("Este product ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el producto",
                    Body = string.Format("Ocurrio un error al eliminar el producto. Detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                product.UpdDate = DateTime.Now.ToLocal();
                product.UpdUser = User.Identity.Name;

                db.Entry(product).State = EntityState.Modified;
                db.Entry(product).Property(c => c.InsDate).IsModified = false;
                db.Entry(product).Property(c => c.InsUser).IsModified = false;
                db.Entry(product).Property(c => c.IsActive).IsModified = false;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al modificar el product",
                    Body = string.Format("Ocurrio un error al guardar los cambios del product {0}. Detalle del error {1}",
                                        product.Description, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del producto guardados",
                Body = string.Format("Los datos del producto {0} fueron modificados", product.Description),
                Id = product.ProductId
            });
        }


        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = new Product();
            try
            {
                product = db.Products.FirstOrDefault(c => c.ProductId == id && c.IsActive);

                if (product != null)
                {
                    product.UpdUser = User.Identity.Name;
                    product.UpdDate = DateTime.Now.ToLocal();
                    product.IsActive = false;

                    db.Entry(product).State = EntityState.Modified;

                    db.Entry(product).Property(c => c.InsUser).IsModified = false;
                    db.Entry(product).Property(c => c.InsDate).IsModified = false;

                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "No existe el producto a eliminar!",
                        Body = string.Format("Este producto ya no esta activo en el catálgo")
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el product",
                    Body = string.Format("Ocurrio un error al eliminar el producto detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Producto eliminado!",
                Body = string.Format("El producto {0} fue eliminado del catálogo", product.Description),
                Id = product.ProductId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleateProduct(string filter)
        {
            var products = db.Products.Where(p => p.Description.Contains(filter)).OrderBy(p => p.Description).Take(Cons.AutoCompleateRows).
                Select(p => new { Label = p.Description, Id = p.ProductId, Value = p.Code });

            return Json(products);
        }

        #endregion

       
    }
}