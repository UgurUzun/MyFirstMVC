using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class ProjectsController : Controller
    {
        // GET: Admin/Projects
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);
            }
        }

        public ActionResult Create()
        {
            var project = new Project();
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            }
            return View(project);
        }

        [HttpPost]
        [ValidateInput(false)]//bu action a artık html/script etiketleri gönderebilir.
        public ActionResult Create(Project project, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    //Dosyayı upload etmeyi dene
                    try
                    {
                        //Yüklenen dosyanın adını entity deki alana ata.
                        project.Photo = UploadFile(upload); //Bizim oluşturduğumuz metod kullanıldı.
                    }
                    catch (Exception ex)
                    {
                        //upload sırasında hata oluşursa view de görüntülemek üzere hatayı Değişkene ekle
                        ViewBag.Error = ex.Message;
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
                        //hata oluştuğu için hatayı eklemek yerine tekrar view' ı tekrar göster ve metottan çık
                        return View(project);
                    }
                    db.Projects.Add(project);//Veri tabanına eklenir
                    db.SaveChanges();//Veri tabanındaki değişiklikler kaydedilir.
                    return RedirectToAction("Index");
                }
            }

            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            }
            return View(project);
        }

        public string UploadFile(HttpPostedFileBase upload) //Bizim oluşturduğumuz method
        {
            //Yüklenmek istenen dosya var mı?
            if (upload != null && upload.ContentLength > 10)
            {
                //Dosya uzantısını kontrol et
                var extension = Path.GetExtension(upload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
                {
                    //uzantı doğruysa dosyanın yükleneceği Uploads dizini var mı kontrol et.
                    if (Directory.Exists(Server.MapPath(" ~/Uploads")))
                    {

                        //Dosya adındaki geçersiz karakterleri düzelt.
                        string fileName = upload.FileName.ToLower();
                        fileName = fileName.Replace("İ", "i");
                        fileName = fileName.Replace("Ş", "s");
                        fileName = fileName.Replace("ı", "i");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(",", "");
                        fileName = fileName.Replace("ö", "o");
                        fileName = fileName.Replace("ü", "u");
                        fileName = fileName.Replace("`", "");
                        fileName = fileName.Replace("Ğ", "g");
                        fileName = fileName.Replace("ğ", "g");

                        //Aynı isimde dosya olmaması için dosya adının önüne zaman pulu ekliyoruz.
                        fileName = DateTime.Now.Ticks.ToString() + fileName;

                        //Dosyayı Uploads dizinine yükle
                        upload.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));

                        //Yüklenen dosyanın adını geri döndür.
                        return fileName;
                    }

                    else
                    {
                        throw new Exception("Uploads dizini mevcut değil.");
                    }
                }
                else
                {
                    throw new Exception("Dosya uzantısı jpeg,jpg,png,gif olmak zorundadır.");
                }
            }
            return null;
        }


        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (projects != null)
                {
                    db.Projects.Remove(projects);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    }
}