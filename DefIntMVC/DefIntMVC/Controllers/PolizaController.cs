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
    public class PolizaController : Controller
    {
        private BDProDefIntEntities db = new BDProDefIntEntities();

        // GET: Poliza
        public ActionResult Index()
        {
            var polizaVehicular = db.polizaVehicular.Include(p => p.Cliente).Include(p => p.Funcionario).Include(p => p.tipoPoliza);
            return View(polizaVehicular.ToList());
        }

        // GET: Poliza/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            polizaVehicular polizaVehicular = db.polizaVehicular.Find(id);
            if (polizaVehicular == null)
            {
                return HttpNotFound();
            }
            return View(polizaVehicular);
        }

        // GET: Poliza/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "Nombre");
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre");
            ViewBag.idTipoPoliza = new SelectList(db.tipoPoliza, "idTipoPoliza", "NombreTP");
            return View();
        }

        // POST: Poliza/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPoliza,Tipo,Modelo,Año,Color,Placa,Chasis,Costo,idFuncionario,idCliente,idTipoPoliza")] polizaVehicular polizaVehicular)
        {
            if (ModelState.IsValid)
            {
                db.polizaVehicular.Add(polizaVehicular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "Nombre", polizaVehicular.idCliente);
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", polizaVehicular.idFuncionario);
            ViewBag.idTipoPoliza = new SelectList(db.tipoPoliza, "idTipoPoliza", "NombreTP", polizaVehicular.idTipoPoliza);
            return View(polizaVehicular);
        }

        // GET: Poliza/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            polizaVehicular polizaVehicular = db.polizaVehicular.Find(id);
            if (polizaVehicular == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "Nombre", polizaVehicular.idCliente);
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", polizaVehicular.idFuncionario);
            ViewBag.idTipoPoliza = new SelectList(db.tipoPoliza, "idTipoPoliza", "NombreTP", polizaVehicular.idTipoPoliza);
            return View(polizaVehicular);
        }

        // POST: Poliza/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPoliza,Tipo,Modelo,Año,Color,Placa,Chasis,Costo,idFuncionario,idCliente,idTipoPoliza")] polizaVehicular polizaVehicular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(polizaVehicular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "Nombre", polizaVehicular.idCliente);
            ViewBag.idFuncionario = new SelectList(db.Funcionario, "idFuncionario", "Nombre", polizaVehicular.idFuncionario);
            ViewBag.idTipoPoliza = new SelectList(db.tipoPoliza, "idTipoPoliza", "NombreTP", polizaVehicular.idTipoPoliza);
            return View(polizaVehicular);
        }

        // GET: Poliza/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            polizaVehicular polizaVehicular = db.polizaVehicular.Find(id);
            if (polizaVehicular == null)
            {
                return HttpNotFound();
            }
            return View(polizaVehicular);
        }

        // POST: Poliza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            polizaVehicular polizaVehicular = db.polizaVehicular.Find(id);
            db.polizaVehicular.Remove(polizaVehicular);
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
