using MtFuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MtFuji.Controllers
{
    public class CustomerAccountController : Controller
    {
        MVCtutorialEntities1 db = new MVCtutorialEntities1();
        //
        // GET: /CustomerAccount/
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public JsonResult SaveData(Customer model)
        {
            string result = "";
            var email = db.Customers.SingleOrDefault(x => x.Email == model.Email);
            if (email != null)
            {
                result = "Fail";

            }
            else
            {
                Customer cus = new Customer();
                cus.Name = model.Name;
                cus.Password = model.Password;
                cus.Email = model.Email;
                cus.Phone = model.Phone;
                cus.Address = model.Address;
                cus.Created = DateTime.Now;
                cus.IsValid = false;
                db.Customers.Add(cus);
                db.SaveChanges();
                BuildEmailTemplate(cus.CustomerID);
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            Customer data = db.Customers.Where(x => x.CustomerID == regId).FirstOrDefault();
            data.IsValid = true;
            db.SaveChanges();
            var msg = "Your Email Is Verified! Congratulation";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = db.Customers.Where(x => x.CustomerID == regID).FirstOrDefault();
            var url = " http://mtfujimyanmar.somee.com/" + "CustomerAccount/Confirm?regId=" + regID;
            //var url = "http://localhost:9287/" + "CustomerAccount/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.Confirmationlink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "simsuum@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);


        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("simsuum1997@gmail.com", "alphatango$P1*2019*");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult CheckValidUser(Customer model)
        {
            string result = "Fail";
            var DataItem = db.Customers.Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();
            if (DataItem != null)
            {
                if (DataItem.IsValid != true)
                {
                    result = "Invalid";
                }
                else
                {
                    Session["UserID"] = DataItem.CustomerID.ToString();
                    Session["name"] = DataItem.Name.ToString();
                    Session["address"] = DataItem.Address.ToString();
                    Session["phone1"] = DataItem.Phone.ToString();

                    result = "Success";
                }


            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult logOut()
        {
            Session.Remove("UserID");
            Session.Remove("name");
            Session.Remove("address");
            Session.Remove("phone1");
            return View("LogIn");
        }
    }
}