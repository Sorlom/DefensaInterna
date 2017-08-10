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
    public class SiniestroController : Controller
    {
        private BDProDefIntEntities db = new BDProDefIntEntities();

        // GET: Siniestro
        public ActionResult Index()
        {
            var siniestro = db.Siniestro.Include(s => s.Funcionario).Include(s => s.polizaVehicular);
            return View(siniestro.ToList());
        }
        // GET: Gastos
        public ActionResult Gastos(int? id)
        {            
            TempData["TPoliza"] = db.polizaVehicular.Where(x => x.idPoliza == id).FirstOrDefault().idTipoPoliza;
            TempData["idSiniestro"] = db.Siniestro.Where(x => x.idPoliza == id).FirstOrDefault().idSiniestro;
            int idSin = int.Parse(TempData["idSiniestro"].ToString());
            TempData["SinEst"] = db.Siniestro.Where(x => x.idSiniestro == idSin).FirstOrDefault().Estado;
            return RedirectToAction("Create", "DetalleGasto");
        }

        // GET: Siniestro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siniestro siniestro = db.Siniestro.Find(id);
            if (siniestro == null)
            {
                return HttpNotFound();
            }
            return View(siniestro);
        }

        // GET: Siniestro/Create
        public ActionResult Create()
        {
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre");
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Placa");
            return View();
        }

        // POST: Siniestro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSiniestro,fechaInicio,Lugar,Detalle,dañosMateriales,dañosPersonales,fechaFin,Fotografia,Costo,Estado,DescripcionCierre,idPoliza,idFuncionario")] Siniestro siniestro,HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                string estado = db.polizaVehicular.Where(x => x.idPoliza == siniestro.idPoliza).FirstOrDefault().Estado;
                if (estado == "Habilitado")
                {
                    if (image1 != null)
                    {
                        siniestro.Fotografia = new byte[image1.ContentLength];
                        image1.InputStream.Read(siniestro.Fotografia, 0, image1.ContentLength);
                    }
                    siniestro.Estado = "Abierto";
                    db.Siniestro.Add(siniestro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", siniestro.idFuncionario);
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Tipo", siniestro.idPoliza);
            return View(siniestro);
        }

        // GET: Siniestro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siniestro siniestro = db.Siniestro.Find(id);
            if (siniestro == null)
            {
                return HttpNotFound();
            }
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", siniestro.idFuncionario);
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Placa", siniestro.idPoliza);
            return View(siniestro);
        }

        // POST: Siniestro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSiniestro,fechaInicio,Lugar,Detalle,dañosMateriales,dañosPersonales,fechaFin,Fotografia,Costo,Estado,DescripcionCierre,idPoliza,idFuncionario")] Siniestro siniestro, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    siniestro.Fotografia = new byte[image1.ContentLength];
                    image1.InputStream.Read(siniestro.Fotografia, 0, image1.ContentLength);
                }
                siniestro.Estado = "Abierto";
                db.Entry(siniestro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", siniestro.idFuncionario);
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Tipo", siniestro.idPoliza);
            return View(siniestro);
        }
        // GET: Siniestro/Edit/5
        public ActionResult Cerrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siniestro siniestro = db.Siniestro.Find(id);
            if (siniestro == null)
            {
                return HttpNotFound();
            }
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", siniestro.idFuncionario);
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Tipo", siniestro.idPoliza);
            return View(siniestro);
        }

        // POST: Siniestro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar([Bind(Include = "idSiniestro,fechaInicio,Lugar,Detalle,dañosMateriales,dañosPersonales,fechaFin,Fotografia,Costo,Estado,DescripcionCierre,idPoliza,idFuncionario")] Siniestro siniestro)
        {
            if (ModelState.IsValid)
            {
                if (siniestro.Estado == "Abierto")
                {
                    siniestro.Estado = "Cerrado";
                    siniestro.fechaFin = DateTime.Today;
                }
                db.Entry(siniestro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", siniestro.idFuncionario);
            ViewBag.idPoliza = new SelectList(db.polizaVehicular, "idPoliza", "Tipo", siniestro.idPoliza);
            return View(siniestro);
        }

        // GET: Siniestro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siniestro siniestro = db.Siniestro.Find(id);
            if (siniestro == null)
            {
                return HttpNotFound();
            }
            return View(siniestro);
        }

        // POST: Siniestro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siniestro siniestro = db.Siniestro.Find(id);
            db.Siniestro.Remove(siniestro);
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
