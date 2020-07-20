using MtFuji.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MtFuji.Controllers
{
    public class AdminController : Controller
    {
        MVCtutorialEntities1 db = new MVCtutorialEntities1();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProduct()
        {
            List<Category> catlist = db.Categories.ToList();
            ViewBag.category = new SelectList(catlist, "CategoryID", "Name");
            var model = db.Products.OrderBy(x => x.product_Id).ToList();//db.Products.Where(x=>x.isDeleted==false).ToList();
            ViewBag.ProductList = model;
            return View();
        }
        public JsonResult GetSubCategory(int categoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> sublist = db.SubCategories.Where(x => x.CategoryID == categoryId).ToList();
            return Json(sublist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveP(ProductVM model, IEnumerable<HttpPostedFileBase> file)
        {
            if (model.product_Id > 0)
            {
                try
                {
                    Product pvm = new Product();
                    string upload = string.Empty;
                    foreach (var item in file)
                    {
                        if (item == null)
                        {
                            break;
                        }
                        string filename = Guid.NewGuid() + Path.GetExtension(item.FileName);
                        string filepath = "~/Images/" + filename;
                        item.SaveAs(Path.Combine(Server.MapPath("~/Images"), filename));
                        upload += filepath + ":";
                    }
                    string[] patharray = upload.Split(':');
                    Product prod = db.Products.SingleOrDefault(x => x.product_Id == model.product_Id && x.isDeleted == false);
                    if (patharray[0] == "" || patharray[1] == "" || patharray[2] == "" || patharray[3] == "")
                    {

                        prod.product_name = model.product_name;
                        prod.product_description = model.product_description;
                        prod.is_new = model.is_new;
                        prod.is_discount = null;
                        prod.new_price = model.new_price;
                        prod.old_price = model.old_price;
                        prod.model_No = model.model_No;
                        prod.quantity = model.quantity;
                        prod.offer_title = model.offer_title;
                        prod.star = 3;
                        prod.CategoryID = model.CategoryID;
                        prod.SubCategoryID = model.SubCategoryID;

                        db.SaveChanges();
                        Console.WriteLine("edit without file");

                    }
                    else
                    {
                        prod.product_name = model.product_name;
                        prod.product_description = model.product_description;
                        prod.is_new = model.is_new;
                        prod.is_discount = null;
                        prod.new_price = model.new_price;
                        prod.old_price = model.old_price;
                        prod.img1 = patharray[0].ToString();
                        prod.img2 = patharray[1].ToString();
                        prod.img3 = patharray[2].ToString();
                        prod.img4 = patharray[3].ToString();
                        prod.model_No = model.model_No;
                        prod.quantity = model.quantity;
                        prod.offer_title = model.offer_title;
                        prod.star = 3;
                        prod.CategoryID = model.CategoryID;
                        prod.SubCategoryID = model.SubCategoryID;

                        db.SaveChanges();
                        Console.WriteLine("edit with file");
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                try
                {
                    Product pvm = new Product();
                    string upload = string.Empty;
                    foreach (var item in file)
                    {
                        if (item == null)
                        {
                            break;
                        }
                        string filename = Guid.NewGuid() + Path.GetExtension(item.FileName);
                        string filepath = "~/Images/" + filename;
                        item.SaveAs(Path.Combine(Server.MapPath("~/Images"), filename));
                        upload += filepath + ":";
                    }
                    string[] patharray = upload.Split(':');

                    pvm.product_name = model.product_name;
                    pvm.product_description = model.product_description;
                    pvm.is_new = model.is_new;
                    pvm.is_discount = null;
                    pvm.new_price = model.new_price;
                    pvm.old_price = model.old_price;
                    pvm.img1 = patharray[0].ToString();
                    pvm.img2 = patharray[1].ToString();
                    pvm.img3 = patharray[2].ToString();
                    pvm.img4 = patharray[3].ToString();
                    pvm.model_No = model.model_No;
                    pvm.quantity = model.quantity;
                    pvm.offer_title = model.offer_title;
                    pvm.star = 3;
                    pvm.CategoryID = model.CategoryID;
                    pvm.SubCategoryID = model.SubCategoryID;
                    pvm.isDeleted = false;
                    db.Products.Add(pvm);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("GetProduct");
        }

        public ActionResult CustomerOrder()
        {
            ViewBag.OrderList = db.CustomerOrders.OrderBy(x => x.OrderDate).ToList();
            return View();
        }

        public ActionResult OrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.CustomerOrders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View("CustomerOrder", order);
        }

        public ActionResult EditProduct(int id)
        {
            List<Category> catList = db.Categories.ToList();
            ViewBag.category = new SelectList(catList, "CategoryID", "Name");
            ProductVM pvm = new ProductVM();
            if (id > 0)
            {
                Product pro = db.Products.SingleOrDefault(x => x.product_Id == id);
                pvm.product_Id = pro.product_Id;
                pvm.product_name = pro.product_name;
                pvm.model_No = pro.model_No;
                pvm.product_description = pro.product_description;
                pvm.is_new = pro.is_new;
                pvm.new_price = pro.new_price;
                pvm.old_price = pro.old_price;
                pvm.quantity = pro.quantity;
                pvm.offer_title = pro.offer_title;
                pvm.model_No = pro.model_No;
                pvm.CategoryID = pro.CategoryID;
                pvm.SubCategoryID = pro.SubCategoryID;
                pvm.isDeleted = pro.isDeleted;

            }

            return View(pvm);
        }
    }
}