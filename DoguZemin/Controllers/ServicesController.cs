using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DoguZemin.Filters;
using Entities;

namespace DoguZemin.Controllers
{
    [Auth]
    public class ServicesController : Controller
    {
        ServicesManager servicesManager = new ServicesManager();
        public ActionResult Index()
        {
            return View(servicesManager.ListQueryable().OrderByDescending(x => x.CreatedOn));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = servicesManager.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Services services, HttpPostedFileBase ServicesImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ServicesImage != null && (ServicesImage.ContentType == "image/jpg" ||
                                            ServicesImage.ContentType == "image/png" ||
                                            ServicesImage.ContentType == "image/jpeg"))
                {
                    string filename = $"services_{services.Title}.{ServicesImage.ContentType.Split('/')[1]}";
                    ServicesImage.SaveAs(Server.MapPath($"~/images/services/{filename}"));
                    services.ServicesImage = filename;
                }

                servicesManager.Insert(services);
                return RedirectToAction("Index");
            }

            return View(services);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = servicesManager.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Services services, HttpPostedFileBase ServicesImage)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Services ser = servicesManager.Find(x => x.Id == services.Id);

                if (ServicesImage != null && (ServicesImage.ContentType == "image/jpg" ||
                                             ServicesImage.ContentType == "image/png" ||
                                             ServicesImage.ContentType == "image/jpeg"))
                {

                    string filename = $"services_{services.Id}.{ServicesImage.ContentType.Split('/')[1]}";
                    ServicesImage.SaveAs(Server.MapPath($"~/images/services/{filename}"));
                    ser.ServicesImage = filename;
                }
                ser.Title = services.Title;
                ser.Description = services.Description;

                servicesManager.Update(services);
                return RedirectToAction("Index");
            }
            return View(services);
        }

        [AuthAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = servicesManager.Find(x => x.Id == id.Value);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [AuthAdmin]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = servicesManager.Find(x => x.Id == id);
            servicesManager.Delete(services);
            System.IO.File.Delete(Server.MapPath($"~/images/services/{services.ServicesImage}"));
            return RedirectToAction("Index");
        }

    }
}
