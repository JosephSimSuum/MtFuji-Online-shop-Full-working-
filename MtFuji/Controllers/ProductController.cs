using MtFuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MountFuji.Models;

namespace MtFuji.Controllers
{
    public class ProductController : Controller
    {
        MVCtutorialEntities1 db = new MVCtutorialEntities1();
        private string strCart = "recent";
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            //var productlist = db.Products.OrderBy(x => x.product_Id).ToList();
            var productlist = db.Products.OrderBy(x => x.product_Id).Where(y => y.isDeleted == false).ToPagedList(pageNumber, pageSize);

            return View(productlist);
        }
        public ActionResult GetProductsByCategory(string categoryName, int? page)
        {
            ViewBag.category = categoryName;
            ViewBag.Categories = db.Categories.Select(x => x.Name).ToList();
            var prods = db.Products.Where(x => x.SubCategory.Name == categoryName && x.isDeleted == false).ToList();
            return View("Product", prods.ToPagedList(page ?? 1, 6));
        }
        [HttpPost]
        public ActionResult FilterByPrice(FormCollection frc, int? page)
        {
            string categoryName = frc["categoryName"];
            int minPrice = int.Parse(frc["minPrice"].ToString());
            int maxPrice = int.Parse(frc["maxPrice"].ToString());
            // ViewBag.filterByPrice = true;

            //  string categoryNam = frc.GetValue("categoryName").ToString();
            var filterProducts = db.Products.Where(y => y.new_price >= minPrice && y.new_price <= maxPrice && y.SubCategory.Name == categoryName).ToList();



            return View("Product", filterProducts.ToPagedList(page ?? 1, 6));
        }
        public ActionResult ProductDetail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {

                //recent cart
                if (Session[strCart] == null)
                {
                    List<Carts> lsCart = new List<Carts>
                {
                    new Carts(db.Products.Find(id),1)
                };
                    Session[strCart] = lsCart;
                }
                else
                {
                    List<Carts> lsCart = (List<Carts>)Session[strCart];
                    int check = isExist(id);
                    if (check == -1)
                    {
                        lsCart.Add(new Carts(db.Products.Find(id), 1));
                    }
                    else
                    {
                        lsCart[check].p++;
                        Session[strCart] = lsCart;
                    }

                    Session[strCart] = lsCart;
                }
                //end

                Product product = db.Products.SingleOrDefault(x => x.product_Id == id);
                ProductVM pvm = new ProductVM();
                pvm.product_Id = product.product_Id;
                pvm.product_name = product.product_name;
                pvm.product_description = product.product_description;
                pvm.is_new = product.is_new;
                pvm.is_discount = product.is_discount;
                pvm.new_price = product.new_price;
                pvm.old_price = product.old_price;
                pvm.offer_title = product.offer_title;
                pvm.img1 = product.img1;
                pvm.img2 = product.img2;
                pvm.img3 = product.img3;
                pvm.img4 = product.img4;
                pvm.model_No = product.model_No;
                pvm.star = product.star;
                ViewBag.alsoBuy = db.Products.Where(x => x.SubCategoryID == product.SubCategoryID).OrderBy(x => x.product_Id).Take(6).ToList();
                return View(pvm);


            }

        }
        private int isExist(int? id)
        {
            List<Carts> lsCart = (List<Carts>)Session[strCart];
            if (lsCart != null)
            {
                for (int i = 0; i < lsCart.Count; i++)
                {
                    if (lsCart[i].product.product_Id == id) return i;
                }

            }
            return -1;
        }

        public ActionResult alsoBuy()
        {

            return View();
        }
    }
}