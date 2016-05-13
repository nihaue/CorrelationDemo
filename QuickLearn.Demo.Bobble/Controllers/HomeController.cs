using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickLearn.Demo.Bobble.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Reset()
        {
            PrintApiController.BobbleState.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Refresh()
        {
            return RedirectToAction("Index");
        }
    }
}