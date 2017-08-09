using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefIntMVC.Models;

namespace DefIntMVC.Controllers
{
    public class DetalleGastoController : Controller
    {
        private BDProDefIntEntities db = new BDProDefIntEntities();

        // GET: DetalleGasto
        public ActionResult Index()
        {
            var detalleGasto = db.DetalleGasto.Include(d => d.Siniestro);
            return View(detalleGasto.ToList());
        }

        // GET: DetalleGasto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleGasto detalleGasto = db.DetalleGasto.Find(id);
            if (detalleGasto == null)
            {
                return HttpNotFound();
            }
            return View(detalleGasto);
        }

        // GET: DetalleGasto/Create
        public ActionResult Create()
        {
            ViewBag.idSiniestro = new SelectList(db.Siniestro, "idSiniestro", "Lugar");
            return View();
        }

        // POST: DetalleGasto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalleGasto,Sepelio,imagenSepelio,atencionMedica,imagenAtMedic,dañosPropiedad,imagenDProp,muerteAccidente,imagenMuerteAcc,perdidaTotal,imagenPerdTotal,reparacionVehicular,imagenRepVehicular,idSiniestro")] DetalleGasto detalleGasto)
        {
            if (ModelState.IsValid)
            {
                db.DetalleGasto.Add(detalleGasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSiniestro = new SelectList(db.Siniestro, "idSiniestro", "Lugar", detalleGasto.idSiniestro);
            return View(detalleGasto);
        }

        // GET: DetalleGasto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleGasto detalleGasto = db.DetalleGasto.Find(id);
            if (detalleGasto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSiniestro = new SelectList(db.Siniestro, "idSiniestro", "Lugar", detalleGasto.idSiniestro);
            return View(detalleGasto);
        }

        // POST: DetalleGasto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalleGasto,Sepelio,imagenSepelio,atencionMedica,imagenAtMedic,dañosPropiedad,imagenDProp,muerteAccidente,imagenMuerteAcc,perdidaTotal,imagenPerdTotal,reparacionVehicular,imagenRepVehicular,idSiniestro")] DetalleGasto detalleGasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleGasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSiniestro = new SelectList(db.Siniestro, "idSiniestro", "Lugar", detalleGasto.idSiniestro);
            return View(detalleGasto);
        }

        // GET: DetalleGasto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleGasto detalleGasto = db.DetalleGasto.Find(id);
            if (detalleGasto == null)
            {
                return HttpNotFound();
            }
            return View(detalleGasto);
        }

        // POST: DetalleGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleGasto detalleGasto = db.DetalleGasto.Find(id);
            db.DetalleGasto.Remove(detalleGasto);
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
