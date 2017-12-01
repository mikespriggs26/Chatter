using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chatter.Models;

namespace Chatter.Controllers
{
    public class ChatterTablesController : Controller
    {
        private chatterEntities1 db = new chatterEntities1();

        // GET: ChatterTables
        public ActionResult Index()
        {
            var chatterTables = db.ChatterTables.Include(c => c.AspNetUser);
            return View(chatterTables.ToList());
        }

        public JsonResult TestJson()
        {
            string jsonTest = "{ \"firstName\": \"Bob\", \"lastName\": \"Sauce\", \"children\": [{\"firstName\": \"Barbie\", \"age\": 19 },{\"firstName\": \"Ron\", \"age\": null }] }";

                return Json(jsonTest, JsonRequestBehavior.AllowGet);
            }


        // GET: ChatterTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatterTable chatterTable = db.ChatterTables.Find(id);
            if (chatterTable == null)
            {
                return HttpNotFound();
            }
            return View(chatterTable);
        }

        // GET: ChatterTables/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: ChatterTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chatID,userID,chatMessage,dateCreated")] ChatterTable chatterTable)
        {
            if (ModelState.IsValid)
            {
                db.ChatterTables.Add(chatterTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chatterTable.userID);
            return View(chatterTable);
        }

        // GET: ChatterTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatterTable chatterTable = db.ChatterTables.Find(id);
            if (chatterTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chatterTable.userID);
            return View(chatterTable);
        }

        // POST: ChatterTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chatID,userID,chatMessage,dateCreated")] ChatterTable chatterTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatterTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chatterTable.userID);
            return View(chatterTable);
        }

        // GET: ChatterTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatterTable chatterTable = db.ChatterTables.Find(id);
            if (chatterTable == null)
            {
                return HttpNotFound();
            }
            return View(chatterTable);
        }

        // POST: ChatterTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChatterTable chatterTable = db.ChatterTables.Find(id);
            db.ChatterTables.Remove(chatterTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
