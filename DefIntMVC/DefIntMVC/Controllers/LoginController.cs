using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefIntMVC.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace DefIntMVC.Controllers
{
    public class LoginController : Controller
    {
        private BDProDefIntEntities db = new BDProDefIntEntities();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["DErr"] != null)
            {
                ViewBag.Msg = TempData["DErr"].ToString();
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {

            string acc = fc["Nom"].ToString();
            string pass = GetMD5(fc["Pass"].ToString());
            TempData["Login"] = acc;

            var count = db.Funcionario.Where(x => x.Login == acc && x.Password == pass).Count();
 
            if (count== 0)
            {
                ViewBag.Msg = "Usuario Invalido";
                return View();               
            }
            else
            {
                FormsAuthentication.SetAuthCookie(acc, false);
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            TempData["Login"] = "";
            TempData["IDlogin"] = "";
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
