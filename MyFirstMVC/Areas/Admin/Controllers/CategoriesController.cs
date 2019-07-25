using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories.ToList();
                return View(categories);
            }
        }

        public ActionResult Create()
        {
            var category = new Category();
            return View(category);
        }

        [HttpPost]
        [ValidateInput(false)]//bu action a artık html/script etiketleri gönderebilir.
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
                if (categories != null)
                {
                    db.Categories.Remove(categories);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

                if (category != null)
                {
                    return View(category);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext()) 
                {
                    var oldProject = db.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();

                    if (oldProject != null)
                    {
                        oldProject.CategoryName = category.CategoryName;

                        db.SaveChanges();
                        return RedirectToAction("Index");         
                       
                    }                    
                }
            }

            return View(category);
        }
    }
}