using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Support;
using Argos.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.Controllers
{
    public partial class CatalogController
    {
      
        public ActionResult Products()
        {
            var model = new SearchProductsVM(db);
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchProducts(ProductFilters filter)
        {
            var model = LookFor(filter);
            return PartialView("_ProductList", model);
        }

        [HttpPost]
        public ActionResult BeginAddProduct()
        {
            var model = new ProductVM(db);

            return PartialView("_ProductEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(ProductVM productVm)
        {
            try
            {

                if (productVm.Product.ProductId == Cons.Zero)
                {
                    var branchId = User.Identity.GetBranchId();

                    //si el producto requiere ser almacenado, se crea stock en la sucursal
                    if (productVm.Product.IsStockable)
                    {
                        productVm.Stock.BranchId = branchId;
                        productVm.Product.ProductStocks.Add(productVm.Stock);
                    }
                    db.Products.Add(productVm.Product);
                }
                else
                {
                    //verifico si hay imagenes para eliminar
                    if(productVm.ToDelete.Count() > Cons.Zero)
                    {
                        var toDelete = db.ProductImages.
                                        Where(i => productVm.ToDelete.Contains(i.ProductImageId)).ToList();

                        foreach(var image in toDelete)
                        {
                            //obtengo la ruta fisica del archivo
                            var filePath = Request.MapPath(image.Path);

                            if (FileManager.DeleteImage(filePath))
                                db.ProductImages.Remove(image);
                        }
                    }

                    productVm.Product.UnLock();

                    db.Entry(productVm.Product).State = EntityState.Modified;

                    //excluyo las propiedades q no deben cambiar
                    db.Entry(productVm.Product).Property(p => p.Code).IsModified        = false;
                    db.Entry(productVm.Product).Property(p => p.IsStockable).IsModified = false;
                    db.Entry(productVm.Product).Property(p => p.IsStockable).IsModified = false;
                    db.Entry(productVm.Product).Property(p => p.InsDate).IsModified     = false;
                    db.Entry(productVm.Product).Property(p => p.InsUser).IsModified     = false;
                    db.Entry(productVm.Product).Property(p => p.IsActive).IsModified    = false;
                }
                //guardo todos los cambios del producto y elimino imagenes
                db.SaveChanges();

                //guardo imagenes nuevas
                var newImages = SaveImages(productVm.Product.ProductId, productVm.NewImages);

                if(newImages.Count > Cons.Zero)
                {
                    db.ProductImages.AddRange(newImages);
                    db.SaveChanges();
                }
             
                return Json(new JResponse { Id = productVm.Product.ProductId,
                    Result = Cons.ResponseSuccess, Header="Cambios guardados",Body="Se agregaron los cambios al catálogo de productos" });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult BeginUpdateProduct(int id)
        {
            try
            {
                var model = new ProductVM(db);

                model.Product = db.Products.Include(p => p.ProductImages).Include(p => p.ProductImages).Include(p => p.SubCategory).
                                Include(p => p.ProductStocks).FirstOrDefault(p => p.ProductId == id);


                if (model != null)
                {
                    var json = EvalLock(model.Product);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model.Product).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model.Product).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges(); 

                    var branchId = User.Identity.GetBranchId();

                    model.Complement.SubCategories = db.SubCategories.
                        Where(sc => sc.CategoryId == model.Product.SubCategory.CategoryId).OrderBy(c => c.Name).ToSelectList();

                    model.Complement.CategoryId = model.Product.SubCategory.CategoryId;

                    model.Stock = model.Product.ProductStocks.FirstOrDefault(s => s.BranchId == branchId);

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
        public ActionResult UnLockProduct(int id)
        {
            try
            {
                var product = db.Products.Find(id);

                product.UnLock();
                db.Entry(product).Property(p => p.LockEndDate);
                db.Entry(product).Property(p => p.LockUser);

                db.SaveChanges();

                return Json(new JResponse { Result = Cons.ResponseSuccess, Id = id });
            }
            catch (Exception ex)
            {
                return Json(new JResponse { Result = Cons.ResponseWarning, Id = id,
                    Header ="Error al desbloquear!", Body="Ocurrio un error al remover el bloqueo del producto! "+ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AutoCompleateProduct(string filter)
        {
            var products = db.Products.Where(p => p.Description.Contains(filter)).OrderBy(p => p.Description).Take(Cons.AutoCompleateRows).
                Select(p => new { Label = p.Description, Id = p.ProductId, Value = p.Code });

            return Json(products);
        }

        #region Private Methods
        private List<ProductImage> SaveImages(int productId, List<HttpPostedFileBase> newImages)
        {
            List<ProductImage> list = new List<ProductImage>();

            //guardo las imagenes y creo el registro de cada imagen
            foreach (var img in newImages)
            {
                if (img != null)
                {
                    var path = FileManager.SaveFileDeprecated(img, productId.ToString(), FileType.ProductImage);

                    ProductImage image = new ProductImage
                    {
                        Name = img.FileName,
                        Path = path,
                        Type = img.ContentType,
                        Size = img.ContentLength,
                        ProductId = productId
                    };
                    //agrego cada nuevo registro de imagen a la lista
                    list.Add(image);
                }
            }

            return list;
        }


        private List<ProductVM> LookFor(ProductFilters filter)
        {
            //obtengo la sucursa en sesión
            var branchId = User.Identity.GetBranchId();

            //configuro variables auxiliares
            string[] arr = new List<string>().ToArray();

            //si el filtro de texto tiene datos
            if (filter.Text != null && filter.Text != string.Empty)
            {
                //divido el texto por palabras y lo convierto en un array
                arr = filter.Text.Trim().Split(' ');
            }

            var products = (from p in db.Products.Include(p => p.ProductImages).Include(p => p.ProductStocks).
                            Include(p => p.SubCategory).Include(p => p.SubCategory.Category).Include(p => p.Compatibilities).
                            Include(p => p.Compatibilities.Select(c => c.Model))

                            where (filter.CategoryId == null || p.SubCategory.CategoryId == filter.CategoryId)
                            && (filter.CategoryId == null || p.SubCategoryId == filter.SubCategoryId)
                            && (filter.Text == null || filter.Text == string.Empty || arr.All(s => (p.Code + " " + p.Description + " " + p.TradeMark).Contains(s)))
                            && (filter.ProductId == null || p.ProductId == filter.ProductId)
                            //si algunos de los filtros de auto es requerido, se realiza el filtrado
                            && (filter.Year == null || p.Compatibilities.Where(c => c.CompatibilityId == filter.Year).ToList().Count > Cons.Zero)
                            && (filter.ModelId == null || p.Compatibilities.Where(c => c.ModelId == filter.ModelId).ToList().Count > Cons.Zero)
                            && (filter.MakerId == null || p.Compatibilities.Where(c => c.Model.MakerId == filter.MakerId).ToList().Count > Cons.Zero)

                            select p).OrderBy(s => s.Description).Take(Cons.MaxSearchRows).ToList();

            List<ProductVM> model = new List<ProductVM>();

            products.ForEach(p =>
            {
                ProductVM vm = new ProductVM(db);
                vm.Product = p;
                vm.Stock = p.ProductStocks.FirstOrDefault(s => s.BranchId == branchId) ?? new ProductStock();

                model.Add(vm);
            });

            return model;
        }


        private void RequestImages()
        {
            List<HttpPostedFile> files = new List<HttpPostedFile>();

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                for (int i = Cons.Zero; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    var file = System.Web.HttpContext.Current.Request.Files[i];
                    if (file.ContentLength > Cons.Zero)
                        files.Add(file);
                }
            }
        }
        #endregion

    }
}