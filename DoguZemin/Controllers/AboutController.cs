using BusinessLayer;
using DoguZemin.Filters;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoguZemin.Controllers
{
    [Auth]
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager();
        public ActionResult Index()
        {
            return View(aboutManager.ListQueryable().OrderByDescending(x=>x.CreatedOn));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(About about , HttpPostedFileBase AboutImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if(AboutImage != null && (AboutImage.ContentType == "image/png" ||
                                          AboutImage.ContentType == "image/jpg" ||
                                          AboutImage.ContentType == "image/jpeg"))
                {
                    string filename = $"about_{about.Title}.{AboutImage.ContentType.Split('/')[1]}";
                    AboutImage.SaveAs(Server.MapPath($"~/images/about/{filename}"));
                    about.AboutImage = filename;
                }
                aboutManager.Insert(about);
                return RedirectToAction("Index");
            }

            return View(about);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = aboutManager.Find(x => x.Id == id.Value);
            if(about == null)
            {
                return HttpNotFound();
            }

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(About about , HttpPostedFileBase AboutImage)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                About ab = aboutManager.Find(x => x.Id == about.Id);

                if (AboutImage != null && (AboutImage.ContentType == "image/png" ||
                                          AboutImage.ContentType == "image/jpg" ||
                                          AboutImage.ContentType == "image/jpeg"))
                {
                    string filename = $"about_{about.Id}.{AboutImage.ContentType.Split('/')[1]}";
                    AboutImage.SaveAs(Server.MapPath($"~/images/about/{filename}"));
                    ab.AboutImage = filename;
                }
                ab.Title = about.Title;
                ab.Text = about.Text;

                aboutManager.Update(ab);
                return RedirectToAction("Index");
            }

            return View(about);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = aboutManager.Find(x => x.Id == id.Value);
            if(about == null)
            {
                return HttpNotFound();
            }

            return View(about);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = aboutManager.Find(x => x.Id == id.Value);
            if (about == null)
            {
                return HttpNotFound();
            }

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Delete(int id)
        {
            About about = aboutManager.Find(x => x.Id == id);
            aboutManager.Delete(about);
            System.IO.File.Delete(Server.MapPath($"~/images/about/{about.AboutImage}"));
            return RedirectToAction("Index");
        }
    }
}