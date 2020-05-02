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
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager();

        public ActionResult Index()
        {
            return View(contactManager.List());
        }

        [AuthAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactManager.Find(x => x.Id == id.Value);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Contact c = contactManager.Find(x => x.Id == contact.Id);

                c.Addresses = contact.Addresses;
                c.Phone1 = contact.Phone1;
                c.Phone2 = contact.Phone2;
                c.Email = contact.Email;
                c.Twitter = contact.Twitter;
                c.Instagram = contact.Instagram;
                c.Linkedin = contact.Linkedin;
                c.Youtube = contact.Youtube;

                contactManager.Update(c);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

    }
}
