using DEF.Data.Services;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace DEF.Website.Controllers
{
    public class SpotlightController : SurfaceController
    {
        private ISpotlightService spotlightService;

        public SpotlightController(ISpotlightService spotlightService)
        {
            this.spotlightService = spotlightService;
        }
        public ActionResult Spotlight(IPublishedContent component)
        {
            var model = spotlightService.Get(component);
            return View("~/Views/Partials/_Spotlight.cshtml", model);
        }
    }
}