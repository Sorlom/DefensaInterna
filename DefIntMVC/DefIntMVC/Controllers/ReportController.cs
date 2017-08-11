using DefIntMVC.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefIntMVC.Controllers
{
    [Authorize(Roles = "R1,R4")]
    public class ReportController : Controller
    {
        private BDProDefIntEntities db = new BDProDefIntEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Report1(string reportName)
        {
            return RedirectToAction("ExportTo", new { ReportType = "PDF", nom = reportName });
        }

        public FileResult ExportTo(string ReportType, string nom)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ReportDataSet";
            reportDataSource.Value = db.View_Report1.Where(x => x.NombreTP == nom).ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=PrimerReporte." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        [HttpPost]
        public ActionResult Report2(string reportName)
        {
            return RedirectToAction("ExportTo2", new { ReportType = "PDF", nom = reportName });
        }

        public FileResult ExportTo2(string ReportType, string nom)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report2.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ReportDataSet2";
            reportDataSource.Value = db.View_Report2.Where(x => x.Estado == nom).ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=SegundoReporte." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        [HttpPost]
        public ActionResult Report3(string reportName)
        {
            return RedirectToAction("ExportTo3", new { ReportType = "PDF", nom = reportName });
        }

        public FileResult ExportTo3(string ReportType, string nom)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report3.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ReportDataSet3";
            reportDataSource.Value = db.View_Report3.Where(x => x.Estado == nom).ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=TercerReporte." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        [HttpPost]
        public ActionResult Report4(int reportName)
        {
            return RedirectToAction("ExportTo4", new { ReportType = "PDF", nom = reportName });
        }

        public FileResult ExportTo4(string ReportType, int nom)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report4.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ReportDataSet4";
            reportDataSource.Value = db.View_Report4.Where(x => x.idSiniestro == nom).ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=CuartoReporte." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }


    }
}
