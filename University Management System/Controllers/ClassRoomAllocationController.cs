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
    public class ClassRoomAllocationController : Controller
    {
        private StudentDBContext db = new StudentDBContext();

        // GET: ClassRoomAllocation
        public ActionResult Index()
        {
            var classRoomAllocations = db.ClassRoomAllocations.Include(c => c.Course).Include(c => c.Department).Include(c => c.Room).Include(c => c.Time);
            return View(classRoomAllocations.ToList());
        }

        // GET: ClassRoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(classRoomAllocation);
        }

        // GET: ClassRoomAllocation/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Course_Name");
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptName");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Name");
            return View();
        }

        // POST: ClassRoomAllocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,CourseId,RoomId,TimeId,StartDate,EndDate")] ClassRoomAllocation classRoomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.ClassRoomAllocations.Add(classRoomAllocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Course_Code", classRoomAllocation.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", classRoomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classRoomAllocation.RoomId);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Name", classRoomAllocation.TimeId);
            return View(classRoomAllocation);
        }

        // GET: ClassRoomAllocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Course_Code", classRoomAllocation.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", classRoomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classRoomAllocation.RoomId);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Name", classRoomAllocation.TimeId);
            return View(classRoomAllocation);
        }

        // POST: ClassRoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,CourseId,RoomId,TimeId,StartDate,EndDate")] ClassRoomAllocation classRoomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classRoomAllocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Course_Code", classRoomAllocation.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", classRoomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classRoomAllocation.RoomId);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "Name", classRoomAllocation.TimeId);
            return View(classRoomAllocation);
        }

        // GET: ClassRoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(classRoomAllocation);
        }

        // POST: ClassRoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classRoomAllocation);
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
