using MtFuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MtFuji.Controllers
{
    public class HomeController : Controller
    {
        MVCtutorialEntities1 db = new MVCtutorialEntities1();
        public ActionResult Index()
        {
            ViewBag.sale = "Sale";
            ViewBag.hot = "Hot";
            ViewBag.offers = db.Products.Where(x => x.offer_title == "Sale");
          
                ViewBag.products = db.Products.Where(x => x.offer_title == "Sale");
            

            return View();
        }
        public ActionResult Filters(string items)
        {

            if (items != null)
            {
                ViewBag.products = db.Products.Where(x => x.offer_title == items);
            }
            else
            {
                ViewBag.products = db.Products.Where(x => x.offer_title == "Sale");
            }

            return PartialView("Filters");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}