using DEF.Data.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace DEF.Website.Controllers
{
    public class HomeController : RenderMvcController
    {
        private IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }
        public ActionResult Home()
        {
            var model = homeService.Get();
            return View("~/Views/Home.cshtml", model);
        }
    }
}