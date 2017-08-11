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
           
                ViewBag.idSiniestro = new SelectList(db.Siniestro, "idSiniestro", "idSiniestro");
                return View();           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalleGasto,Sepelio,imagenSepelio,atencionMedica,imagenAtMedic,dañosPropiedad,imagenDProp,muerteAccidente,imagenMuerteAcc,perdidaTotal,imagenPerdTotal,reparacionVehicular,imagenRepVehicular,idSiniestro")] DetalleGasto detalleGasto
            , HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4, HttpPostedFileBase image5, HttpPostedFileBase image6)
        {
                if (ModelState.IsValid)
                {
                if (image1 != null)
                {
                    detalleGasto.imagenSepelio = new byte[image1.ContentLength];
                    image1.InputStream.Read(detalleGasto.imagenSepelio, 0, image1.ContentLength);
                }
                if (image2 != null)
                {
                    detalleGasto.imagenAtMedic = new byte[image2.ContentLength];
                    image2.InputStream.Read(detalleGasto.imagenAtMedic, 0, image2.ContentLength);
                }
                if (image3 != null)
                {
                    detalleGasto.imagenDProp = new byte[image3.ContentLength];
                    image2.InputStream.Read(detalleGasto.imagenDProp, 0, image3.ContentLength);
                }
                if (image4 != null)
                {
                    detalleGasto.imagenMuerteAcc = new byte[image4.ContentLength];
                    image4.InputStream.Read(detalleGasto.imagenMuerteAcc, 0, image4.ContentLength);
                }
                if (image5 != null)
                {
                    detalleGasto.imagenPerdTotal = new byte[image5.ContentLength];
                    image5.InputStream.Read(detalleGasto.imagenPerdTotal, 0, image5.ContentLength);
                }
                if (image6 != null)
                {
                    detalleGasto.imagenRepVehicular = new byte[image6.ContentLength];
                    image6.InputStream.Read(detalleGasto.imagenRepVehicular, 0, image6.ContentLength);
                }
                detalleGasto.idSiniestro = int.Parse(TempData["idSiniestro"].ToString());
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
            var idSin = db.DetalleGasto.Where(x => x.idDetalleGasto == id).FirstOrDefault().idSiniestro;
            TempData["idSin2"] = idSin;
            var idpol = db.Siniestro.Where(x => x.idSiniestro == idSin).FirstOrDefault().idPoliza;
            TempData["TPoliza2"] = db.polizaVehicular.Where(x => x.idPoliza == idpol).FirstOrDefault().idTipoPoliza;
            var SinEst2 = db.Siniestro.Where(x => x.idPoliza == idpol).FirstOrDefault().Estado;

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
        public ActionResult Edit([Bind(Include = "idDetalleGasto,Sepelio,imagenSepelio,atencionMedica,imagenAtMedic,dañosPropiedad,imagenDProp,muerteAccidente,imagenMuerteAcc,perdidaTotal,imagenPerdTotal,reparacionVehicular,imagenRepVehicular,idSiniestro")] DetalleGasto detalleGasto
            , HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4, HttpPostedFileBase image5, HttpPostedFileBase image6)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    detalleGasto.imagenSepelio = new byte[image1.ContentLength];
                    image1.InputStream.Read(detalleGasto.imagenSepelio, 0, image1.ContentLength);
                }
                if (image2 != null)
                {
                    detalleGasto.imagenAtMedic = new byte[image2.ContentLength];
                    image2.InputStream.Read(detalleGasto.imagenAtMedic, 0, image2.ContentLength);
                }
                if (image3 != null)
                {
                    detalleGasto.imagenDProp = new byte[image3.ContentLength];
                    image3.InputStream.Read(detalleGasto.imagenDProp, 0, image3.ContentLength);
                }
                if (image4 != null)
                {
                    detalleGasto.imagenMuerteAcc = new byte[image4.ContentLength];
                    image4.InputStream.Read(detalleGasto.imagenMuerteAcc, 0, image4.ContentLength);
                }
                if (image5 != null)
                {
                    detalleGasto.imagenPerdTotal = new byte[image5.ContentLength];
                    image5.InputStream.Read(detalleGasto.imagenPerdTotal, 0, image5.ContentLength);
                }
                if (image6 != null)
                {
                    detalleGasto.imagenRepVehicular = new byte[image6.ContentLength];
                    image6.InputStream.Read(detalleGasto.imagenRepVehicular, 0, image6.ContentLength);
                }
                detalleGasto.idSiniestro = int.Parse(TempData["idSin2"].ToString());
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
