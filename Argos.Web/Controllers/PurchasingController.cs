using Argos.Common.Constants;
using Argos.Data.Context;
using Argos.Models.BaseTypes;
using Argos.Models.Purchasing;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Argos.Web.Controllers
{
    [Authorize]
    public class PurchasingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchasing
        public ActionResult Index()
        {
            var model = new List<Purchase>();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Purchase();
            model.PurchaseDetails = new List<PurchaseDetail>();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(Purchase purchase)
        {
            return Json(new JResponse { Body = "Completado", Header = "Order de comora", Code = Responses.Codes.Success, Result = Responses.Success });
        }
    }
}