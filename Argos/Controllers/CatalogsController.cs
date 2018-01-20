using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Argos.Models;
using Argos.Models.Catalog;
using System.Web.Mvc;

namespace Argos.Controllers
{
    public class CatalogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}