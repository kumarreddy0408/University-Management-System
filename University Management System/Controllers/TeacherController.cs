using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University_Management_System.Models;


namespace University_Management_System.Controllers
{
    public class TeacherController : Controller
    {
        private StudentDBContext db = new StudentDBContext();

        // GET: /Teacher/
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Department).Include(t => t.Designation);
            return View(teachers.ToList());
        }

        // GET: /Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptName");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Title");
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Email,Contact,DesignationId,DepartmentId,CreditTaken,CreditLeft")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.CreditLeft = teacher.CreditTaken;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                ViewBag.Message = "Teacher Saved Successfully.";
                // return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Title", teacher.DesignationId);
            return View(teacher);
        }

        public JsonResult IsEmailExists(string TeacherEmail)
        {
            return Json(!db.Teachers.Any(m => m.Email == TeacherEmail), JsonRequestBehavior.AllowGet);
        }

        // GET: /Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Title", teacher.DesignationId);
            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Email,Contact,DesignationId,DepartmentId,CreditTaken,CreditLeft")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Title", teacher.DesignationId);
            return View(teacher);
        }

        // GET: /Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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