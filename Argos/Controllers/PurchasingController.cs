using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using Argos.Support;
using Argos.ViewModels.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Argos.Controllers
{
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
    }
}