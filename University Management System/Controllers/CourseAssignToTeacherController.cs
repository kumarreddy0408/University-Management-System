using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using University_Management_System.Models;


namespace University_Management_System.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        private StudentDBContext db = new StudentDBContext();

        // GET: /CourseAssign/
        public ActionResult Index()
        {
            var courseassigns = db.CourseAssignToTeachers.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(courseassigns.ToList());
        }

        public ActionResult ViewCourseStatistics()
        {
            ViewBag.Departments = db.Deparments.ToList();
            return View();
        }

        public JsonResult ShowCourseStatistics(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        // GET: /CourseAssign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseassign = db.CourseAssignToTeachers.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            return View(courseassign);
        }

        // GET: /CourseAssign/Create
        public ActionResult Save()
        {
            //ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode");
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            //ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");

            ViewBag.Departments = db.Deparments.ToList();

            return View();
        }

        public JsonResult GetTeacherByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(m => m.DepartmentId == deptId).ToList();
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherById(int TeacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(m => m.Id == TeacherId);
            return Json(teacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            Course aCourse = db.Courses.FirstOrDefault(m => m.Id == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }

        // POST: /CourseAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,DepartmentId,TeacherId,CreditTaken,CourseID,Name,Credit")] CourseAssignToTeacher courseassign)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssignToTeachers.Add(courseassign);
                db.SaveChanges();
                ViewBag.Message = "Course Assigned Successful";

                //  return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassign.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", courseassign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseassign.TeacherId);
            return View(courseassign);
        }

        public JsonResult SaveCourseAssign(CourseAssignToTeacher courseAssign)
        {
            var checkAssignedCoueses =
                db.CourseAssignToTeachers.Where(m => m.CourseID == courseAssign.CourseID)
                    .ToList();

            if (checkAssignedCoueses.Count > 0)
                return Json(false);

            else
            {
                db.CourseAssignToTeachers.Add(courseAssign);
                db.SaveChanges();

                var teacher = db.Teachers.FirstOrDefault(m => m.Id == courseAssign.TeacherId);

                if (teacher != null)
                {
                    //teacher.CreditLeft = CourseAssignToTeacher.CreditLeft;
                    teacher.CreditLeft = courseAssign.CreditLeft;
                    db.Teachers.AddOrUpdate(teacher);
                    db.SaveChanges();

                    var course = db.Courses.FirstOrDefault(m => m.Id == courseAssign.CourseID);

                    if (course != null)
                    {
                        //course.CourseStatus = true;
                        course.CourseAssignTo = teacher.Name;
                        courseAssign.Name= course.Course_Name;
                        courseAssign.Credit = course.Course_Credit;
                        db.Courses.AddOrUpdate(course);
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
        }

        // GET: /CourseAssign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseassign = db.CourseAssignToTeachers.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassign.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", courseassign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseassign.TeacherId);
            return View(courseassign);
        }

        // POST: /CourseAssign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,TeacherId,CreditTaken,CreditLeft,CourseID,Name,Credit")] CourseAssignToTeacher courseassign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseassign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassign.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Deparments, "Id", "DeptCode", courseassign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseassign.TeacherId);
            return View(courseassign);
        }

        // GET: /CourseAssign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignToTeacher courseassign = db.CourseAssignToTeachers.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            return View(courseassign);
        }

        // POST: /CourseAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssignToTeacher courseassign = db.CourseAssignToTeachers.Find(id);
            db.CourseAssignToTeachers.Remove(courseassign);
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