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
    public class SliderController : Controller
    {
        SliderManager sliderManager = new SliderManager();
        public ActionResult Index()
        {
            return View(sliderManager.ListQueryable().OrderByDescending(x => x.CreatedOn));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderManager.Find(x => x.Id == id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
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
        public ActionResult Create(Slider slider, HttpPostedFileBase SlideImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (SlideImage != null && (SlideImage.ContentType == "image/jpg" ||
                                            SlideImage.ContentType == "image/png" ||
                                            SlideImage.ContentType == "image/jpeg"))
                {
                    string filename = $"slider_{slider.Title}.{SlideImage.ContentType.Split('/')[1]}";
                    SlideImage.SaveAs(Server.MapPath($"~/images/slide/{filename}"));
                    slider.SlideImage = filename;
                }

                sliderManager.Insert(slider);
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderManager.Find(x => x.Id == id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Slider slider, HttpPostedFileBase SlideImage)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Slider sld = sliderManager.Find(x => x.Id == slider.Id);
                if (SlideImage != null && (SlideImage.ContentType == "image/png" ||
                                          SlideImage.ContentType == "image/jpg" ||
                                          SlideImage.ContentType == "image/jpeg"))
                {

                    string filename = $"slider_{sld.Id}.{SlideImage.ContentType.Split('/')[1]}";
                    SlideImage.SaveAs(Server.MapPath($"~/images/slide/{filename}"));
                    sld.SlideImage = filename;
                }
                sld.Title = slider.Title;
                sld.Text = slider.Text;

                sliderManager.Update(sld);
  
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        [AuthAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderManager.Find(x => x.Id == id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [AuthAdmin]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderManager.Find(x => x.Id == id);
            sliderManager.Delete(slider);
            System.IO.File.Delete(Server.MapPath($"~/images/slide/{slider.SlideImage}"));
            return RedirectToAction("Index");
        }
    }
}
