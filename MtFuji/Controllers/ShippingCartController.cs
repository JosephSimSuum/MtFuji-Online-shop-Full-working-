using MountFuji.Models;
using MtFuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MtFuji.Controllers
{
    public class ShippingCartController : Controller
    {
        MVCtutorialEntities1 db = new MVCtutorialEntities1();
        private string strCart = "Cart";
        //
        // GET: /ShippingCart/
        public ActionResult AddtoCart()
        {
            return View();
        }
        //public ActionResult recentView(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    List<Carts> lcart = new List<Carts>
        //    {
        //        new Carts(db.Products.Find(id),1)
        //    };
        //    Session["recent"] = lcart;
            
        //}
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
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
            return View("AddtoCart");
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {

                int check = isExist(id);
                List<Carts> lsCart = (List<Carts>)Session[strCart];
                if (lsCart != null)
                {
                    lsCart.RemoveAt(check);
                }

            }
            return View("AddtoCart");
        }
        public ActionResult updateCart(FormCollection frc)
        {
            string[] qty = frc.GetValues("qty");
            List<Carts> lsCart = (List<Carts>)Session[strCart];
            if (lsCart != null)
            {
                for (int i = 0; i < lsCart.Count; i++)
                {
                    lsCart[i].p = Convert.ToInt32(qty[i]);
                }
                Session[strCart] = lsCart;
            }

            return View("AddtoCart");
        }
        public ActionResult Order()
        {
            return View();
        }

        public ActionResult proceedOrder(FormCollection frc)
        {
            List<Carts> lstcart = (List<Carts>)Session[strCart];
            if (lstcart != null)
            {
                CustomerOrder porder = new CustomerOrder()
                {
                    CustomerName = frc["cusname"],
                    CustomerAddress = frc["cusaddress"],
                    CustomerPhone = frc["cusphone1"],
                    PaymentType = "cash",
                    OrderDate = DateTime.Now,
                    Status = null,
                    Phone2 = frc["cusphone2"]
                };
                db.CustomerOrders.Add(porder);
                db.SaveChanges();


                foreach (Carts cart in lstcart)
                {
                    OrderDetail orderdetail = new OrderDetail()
                    {
                        OrderID = porder.OrderID,
                        ProductID = cart.product.product_Id,
                        Quantity = cart.p,
                        Price = Convert.ToDouble(cart.product.new_price)
                    };
                    db.OrderDetails.Add(orderdetail);
                    db.SaveChanges();

                }
                Session.Remove(strCart);
            }

            return View("CompleteOrder");
        }
        public ActionResult CompleteOrder()
        {
            return View();
        }
    }
}