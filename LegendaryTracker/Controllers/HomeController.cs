using System.Web.Mvc;
using LegendaryTracker.Models;
using LegendaryTracker.Services;

namespace LegendaryTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService gameService;

        public HomeController()
        {
            gameService = new GameService(new ExcelDAL());
        }

        [AuthorizeFilter]
        public ActionResult Dashboard()
        {
            return View();
        }

        [AuthorizeFilter]
        public ActionResult Randomizer()
        {
            var options = gameService.InitOptions();

            return View(options);
        }

        [HttpPost]
        [AuthorizeFilter]
        public ActionResult GetOptions(RandomizerOptionsModel model)
        {
            var result = gameService.Randomize(model);

            return Json(result);
        }
    }
}