using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoguZemin.Controllers
{
    public class HomeController : Controller
    {
        private AboutManager aboutManager = new AboutManager();
        private ServicesManager servicesManager = new ServicesManager();
        private ProjectsDoneManager projectsDoneManager = new ProjectsDoneManager();

        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("Hakkimizda/{baslik}/{id:int}")]
        [HttpGet]
        public ActionResult About(int? id)
        {
            About about = aboutManager.Find(x => x.Id == id.Value);
            return View(about);
        }

        [Route("FaaliyetDetay/{baslik}/{id:int}")]
        [HttpGet]
        public ActionResult Services(int? id)
        {
            Services services = servicesManager.Find(x => x.Id == id.Value);
            return View(services);
        }

        [Route("Galeri")]
        public ActionResult Galery()
        {

            return View("Galery");
        }

        [Route("YapilanProjeler")]
        public ActionResult ProjectsDone()
        {
            return View("ProjectsDone");
        }

        [Route("YapilanProjeDetay/{baslik}/{id:int}")]
        [HttpGet]
        public ActionResult ProjectsDoneDetail(int? id)
        {
            ProjectsDone projectsDone = projectsDoneManager.Find(x => x.Id == id.Value);

            return View(projectsDone);
        }

        [Route("Iletisim")]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult _PartialFooter()
        {
            return PartialView();
        }

    }
}