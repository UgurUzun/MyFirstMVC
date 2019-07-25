using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Projects()
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);
            }

              
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost] //2.action için konuldu.Parametreli olan için
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                    mailMessage.From = new System.Net.Mail.MailAddress("uguruzun135@gmail.com", "Uğur Uzun"); //Gönderilen hesap
                    mailMessage.Subject = "İletişim Formu: " + model.FirstName + model.LastName;

                    mailMessage.To.Add("uguruzun135@gmail.com");

                    string body;
                    body = "Ad Soyad: " + model.FirstName + "<br />";
                    body += "Telefon: " + model.LastName + "<br />";
                    body += "E-posta: " + model.Email + "<br />";
                    body += "Mesaj: " + model.Message + "<br />";
                    body += "Tarih:" + DateTime.Now.ToString("dd MMMM yyyy HH:mm") + "<br/>";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("uguruzun135@gmail.com", "u186:U538:"); //Gönderen
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                    ViewBag.Message = "Mesajınız gönderildi";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Form Gönderimi başarısız oldu.Tekrar Deneyiniz";
                }
            }
            return View(model);
        }
    }
}