using BusinessLayer;
using BusinessLayer.Result;
using DoguZemin.Filters;
using DoguZemin.Models;
using Entities;
using Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoguZemin.Controllers
{
    public class AdminController : Controller
    {
        UserManager userManager = new UserManager();
        ServicesManager servicesManager = new ServicesManager();
        ProjectsDoneManager projectsDoneManager = new ProjectsDoneManager();

        [Auth]
        public ActionResult Index()
        {
            ViewBag.UserCount = userManager.List().Count();
            ViewBag.ServicesCount = servicesManager.List().Count();
            ViewBag.ProjectsDoneCount = projectsDoneManager.List().Count();
            return View("Index");
        }

        [Auth]
        public ActionResult Adminler()
        {
            return View(userManager.List());
        }

        [Auth]
        [HttpGet]
        [AuthAdmin]
        public ActionResult RegisterUser()
        {

            return View();
        }

        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                return RedirectToAction("Adminler");
            }

            return View(model);
        }

        [Auth]
        [AuthAdmin]
        [HttpGet]
        public ActionResult UserEdit(int? id)
        {
            BusinessLayerResult<Users> res = userManager.GetUserById(id.Value);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(res.Result);
            }

            return View(res.Result);
        }

        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UserEdit(Users model, HttpPostedFileBase ProfileImageFile)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                if (ProfileImageFile != null && (ProfileImageFile.ContentType == "image/jpg" ||
                                                ProfileImageFile.ContentType == "image/png" ||
                                                ProfileImageFile.ContentType == "image/jpeg"))
                {
                    string filename = $"user_{model.Id}.{ProfileImageFile.ContentType.Split('/')[1]}";
                    ProfileImageFile.SaveAs(Server.MapPath($"~/images/user/{filename}"));
                    model.ProfilImage = filename;
                }

                BusinessLayerResult<Users> res = userManager.UpdateUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                //CurrentSession.Set<Users>("login",res.Result);
                return RedirectToAction("Adminler");
            }

            return View(model);
        }

        [Auth]
        [AuthAdmin]
        public ActionResult UserDelete(int? id)
        {
            BusinessLayerResult<Users> res = userManager.DeleteUserById(id.Value);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View("Adminler");
            }

            if (id.Value == CurrentSession.User.Id)
            {
                CurrentSession.Remove("login");
                return RedirectToAction("Login");
            }

            return RedirectToAction("Adminler");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                //Session["login"] = res.Result;
                CurrentSession.Set<Users>("login", res.Result);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            CurrentSession.Remove("login");
            //CurrentSession.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ForgetPass(ForgetPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.ForgetPassword(model);
                
                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                ViewBag.ShowMessage = "Şifre başarılı bir şekilde gönderildi. Lütfen e-posta adresinizi kontrol ediniz..";
                return View("ForgetPass");
            }
            return View(model);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}