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
        public ActionResult Create(Project project)
        {
            if(ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            }
            return View(project);
        }


        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if(projects != null)
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