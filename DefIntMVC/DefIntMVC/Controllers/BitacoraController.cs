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
    [Authorize(Roles = "R1")]
    public class BitacoraController : Controller
    {

        private BDProDefIntEntities db = new BDProDefIntEntities();

        // GET: Bitacora
        public ActionResult Index()
        {
            return View(db.Bitacora.ToList());
        }

       
    }
}
