using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUDWithImage.Data;
using CRUDWithImage.Models;
using Microsoft.SqlServer.Server;
namespace CRUDWithImage.Controllers
{
    public class EmployeesController : Controller
    {
        private CRUDWithImageContext db = new CRUDWithImageContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,Email,Image,File")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(employee.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                employee.Image = "~/Images/" + _filename;

                db.Employees.Add(employee);
                if (employee.File.ContentLength < 1000000)
                {
                    if (db.SaveChanges() > 0)
                    {
                        employee.File.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "File must be less than or equal to 1MB";
                }
            }
            return View(employee);
        }
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            Session["imgPath"] = employee.Image;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,Email,Image,File")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.File != null)
                {
                    string filename = Path.GetFileName(employee.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    employee.Image = "~/Images/" + _filename;
                    if (employee.File.ContentLength < 1000000)
                    {
                        db.Entry(employee).State = EntityState.Modified;
                        string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {
                            employee.File.SaveAs(path);
                            if (System.IO.File.Exists(oldImgPath))
                            {
                                System.IO.File.Delete(oldImgPath);
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "File must be less than or equal to 1MB";
                    }
                }
                else
                {
                    employee.Image = Session["imgPath"].ToString();
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            string currentImg = Request.MapPath(employee.Image);
            db.Employees.Remove(employee);
            if (db.SaveChanges()>0)
            {
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
            }
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