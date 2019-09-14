using EFSqlDependencyHelper.EFContextExtensions;
using EFSqlDependencyHelper.SqlServerNotifier;
using GkfxDomain;
using SqlServerDataAccess.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GkWebApp.Controllers
{
    public class HomeController : Controller
    {
        private CurrencyRepository repository;
        public HomeController(CurrencyRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }
        public async Task<ActionResult> Index()
        {
            var db = (CurrencyDbContext)repository.GetContext();
            var collection = db.Currencies;
            ViewBag.NotifierEntity = db.GetNotifierEntity<SqlServerDataAccess.Models.Currency>(collection).ToJson();
            return View(await collection.ToListAsync());
        }
        public async Task<ActionResult> IndexPartial()
        {
            var db = (CurrencyDbContext)repository.GetContext();
            var collection = db.Currencies;
            ViewBag.NotifierEntity = db.GetNotifierEntity<SqlServerDataAccess.Models.Currency>(collection).ToJson();
            return PartialView(await collection.ToListAsync());
        }

    }
}