using BusinessLayer;
using DoguZemin.Filters;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoguZemin.Controllers
{
    [Auth]
    public class ProjectsDoneController : Controller
    {
        ProjectsDoneManager projectsDoneManager = new ProjectsDoneManager();
        ServicesManager servicesManager = new ServicesManager();

        public ActionResult Index()
        {
            return View(projectsDoneManager.ListQueryable().Include("Services").OrderByDescending(x=>x.CreatedOn));
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ServicesId = new SelectList(servicesManager.List(), "Id", "Title");
            return View();
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProjectsDone projectsDone , HttpPostedFileBase SlideImage1, HttpPostedFileBase SlideImage2, HttpPostedFileBase SlideImage3)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (SlideImage1 != null && (SlideImage1.ContentType == "image/jpg" ||
                                            SlideImage1.ContentType == "image/png" ||
                                            SlideImage1.ContentType == "image/jpeg"))
                {
                    string filename1 = $"{Guid.NewGuid()}.{SlideImage1.ContentType.Split('/')[1]}";
                    SlideImage1.SaveAs(Server.MapPath($"~/images/projectsdone/{filename1}"));
                    projectsDone.SlideImage1 = filename1;
                }

                if (SlideImage2 != null && (SlideImage2.ContentType == "image/jpg" ||
                            SlideImage2.ContentType == "image/png" ||
                            SlideImage2.ContentType == "image/jpeg"))
                {
                    string filename2 = $"{Guid.NewGuid()}.{SlideImage2.ContentType.Split('/')[1]}";
                    SlideImage2.SaveAs(Server.MapPath($"~/images/projectsdone/{filename2}"));
                    projectsDone.SlideImage2 = filename2;
                }

                if (SlideImage3 != null && (SlideImage3.ContentType == "image/jpg" ||
                            SlideImage3.ContentType == "image/png" ||
                            SlideImage3.ContentType == "image/jpeg"))
                {
                    string filename3 = $"{Guid.NewGuid()}.{SlideImage3.ContentType.Split('/')[1]}";
                    SlideImage3.SaveAs(Server.MapPath($"~/images/projectsdone/{filename3}"));
                    projectsDone.SlideImage3 = filename3;
                }

                projectsDoneManager.Insert(projectsDone);
                return RedirectToAction("Index");
            }

            ViewBag.ServicesId = new SelectList(servicesManager.List(), "Id", "Title", projectsDone.ServicesId);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectsDone projectsDone = projectsDoneManager.Find(x => x.Id == id.Value);
            if (projectsDone == null)
            {
                return HttpNotFound();
            }
            return View(projectsDone);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectsDone projectsdone = projectsDoneManager.Find(x => x.Id == id.Value);
            ViewBag.ServicesId = new SelectList(servicesManager.List(), "Id", "Title", projectsdone.ServicesId);

            return View(projectsdone);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectsDone projectsDone , HttpPostedFileBase SlideImage1, HttpPostedFileBase SlideImage2, HttpPostedFileBase SlideImage3)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                ProjectsDone pd = projectsDoneManager.Find(x => x.Id == projectsDone.Id);

                if (SlideImage1 != null && (SlideImage1.ContentType == "image/jpg" ||
                                            SlideImage1.ContentType == "image/png" ||
                                            SlideImage1.ContentType == "image/jpeg"))
                {
                    string filename1 = $"{Guid.NewGuid()}.{SlideImage1.ContentType.Split('/')[1]}";
                    SlideImage1.SaveAs(Server.MapPath($"~/images/projectsdone/{filename1}"));
                    pd.SlideImage1 = filename1;
                }

                if (SlideImage2 != null && (SlideImage2.ContentType == "image/jpg" ||
                            SlideImage2.ContentType == "image/png" ||
                            SlideImage2.ContentType == "image/jpeg"))
                {
                    string filename2 = $"{Guid.NewGuid()}.{SlideImage2.ContentType.Split('/')[1]}";
                    SlideImage2.SaveAs(Server.MapPath($"~/images/projectsdone/{filename2}"));
                    pd.SlideImage2 = filename2;
                }

                if (SlideImage3 != null && (SlideImage3.ContentType == "image/jpg" ||
                            SlideImage3.ContentType == "image/png" ||
                            SlideImage3.ContentType == "image/jpeg"))
                {
                    string filename3 = $"{Guid.NewGuid()}.{SlideImage3.ContentType.Split('/')[1]}";
                    SlideImage3.SaveAs(Server.MapPath($"~/images/projectsdone/{filename3}"));
                    pd.SlideImage3 = filename3;
                }

                pd.Title = projectsDone.Title;
                pd.Text = projectsDone.Text;
                pd.ServicesId = projectsDone.ServicesId;
                pd.GiveJob = projectsDone.GiveJob;
                pd.ProjectDate = projectsDone.ProjectDate;
                pd.ProjectUrl = projectsDone.ProjectUrl;

                projectsDoneManager.Update(pd);
                return RedirectToAction("Index");
            }

            ViewBag.ServicesId = new SelectList(servicesManager.List(), "Id", "Title", projectsDone.ServicesId);
            return View();
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectsDone projectsDone = projectsDoneManager.Find(x => x.Id == id.Value);
            if (projectsDone == null)
            {
                return HttpNotFound();
            }
            return View(projectsDone);
        }

        [AuthAdmin]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectsDone projectsDone = projectsDoneManager.Find(x => x.Id == id);
            projectsDoneManager.Delete(projectsDone);
            System.IO.File.Delete(Server.MapPath($"~/images/projectsdone/{projectsDone.SlideImage1}"));
            System.IO.File.Delete(Server.MapPath($"~/images/projectsdone/{projectsDone.SlideImage2}"));
            System.IO.File.Delete(Server.MapPath($"~/images/projectsdone/{projectsDone.SlideImage3}"));
            return RedirectToAction("Index");
        }
    }
}