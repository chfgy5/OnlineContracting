using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineContracting.Models;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text.pdf;

namespace OnlineContracting.Controllers
{
    [Authorize(Roles = "Agent, Admin, SuperAdmin")]
    public class AgentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Gets index view, if they have submitted a contract
        public ActionResult Index(string Id)
        {
            if (!System.IO.File.Exists("C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/MutualContract" + Id + ".pdf"))
                return RedirectToAction("ContractForm", "Agent", new { id = Id });

            return View();
        }

        // Gets contract form
        public ActionResult ContractForm(string id)
        {
            var model = db.Users.Find(id);
            return View(model);
        }

        // Method 1: https://www.codeproject.com/Tips/697733/Display-PDF-within-web-browser-using-MVC
        // Displays contract in window
        public ActionResult DisplayContract(string filePath)
        {
            /* if relative path do: https://stackoverflow.com/questions/32329268/checking-if-file-exists-in-asp-net-mvc-5
             *  var absolutePath = HttpContext.Server.MapPath(relativePath);
             */
            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "application/pdf");
            }
            return new HttpNotFoundResult("File Not Found");
        }

        // https://johnnycode.com/2010/03/05/using-a-template-to-programmatically-create-pdfs-with-c-and-itextsharp/
        // Creates new all pdfs based on user id, populates them with values from form
        public ActionResult Generate(FormCollection formValues, string Id)
        {
            var TemplatePaths = new string[] { "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/AetnaContract.pdf", "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/MedicoContract.pdf",
                "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/MutualContract.pdf", "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/TransamericaPremierContract.pdf" };

            var NewFileNames = new string[] { "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/AetnaContract"+Id+".pdf", "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/MedicoContract"+Id+".pdf",
                "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/MutualContract"+Id+".pdf", "C:/wamp/www/OnlineContracting/OnlineContracting/PDFs/TransamericaPremierContract"+Id+".pdf" };
            for (var i = 0; i < TemplatePaths.Length; i++)
            {
                using (var existingFileStream = new FileStream(TemplatePaths[i], FileMode.Open))
                using (var newFileStream = new FileStream(NewFileNames[i], FileMode.Create))
                {
                    // Open existing PDF
                    var pdfReader = new PdfReader(existingFileStream);

                    // PdfStamper, which will create
                    var stamper = new PdfStamper(pdfReader, newFileStream);

                    var form = stamper.AcroFields;
                    var fieldKeys = form.Fields.Keys;

                    foreach (string fieldKey in fieldKeys)
                    {
                        if (formValues[fieldKey] != null && formValues[fieldKey] != "")
                        {
                            form.SetField(fieldKey, formValues[fieldKey]);
                        }
                        if (formValues[fieldKey] == "true,false")
                        {
                            form.SetField(fieldKey, "Yes");
                        }
                    }
                    // Will Fail half the time. Do we care? NO
                    form.SetField("Name", formValues["FName"] + " " + formValues["MI"] + " " + formValues["LName"] + " " + formValues["Suffix"]);
                    form.SetField("SSN", formValues["SSN1"] + "-" + formValues["SSN2"] + "-" + formValues["SSN3"]);
                    // Fills out apt number if no seperate spot for it
                    if (!fieldKeys.Contains("Apt") && formValues["Apt"] != "")
                    {
                        form.SetField("HomeAddress", formValues["HomeAddress"] + ", Apt. " + formValues["Apt"]);
                    }
                    // Checks box for gender on aetna contract. yes thats all.
                    if (formValues["Gender"] == "M" || formValues["Gender"] == "m")
                    {
                        form.SetField("Male", "Yes");
                    }
                    else if (formValues["Gender"] == "F" || formValues["Gender"] == "f")
                    {
                        form.SetField("Female", "Yes");
                    }
                    // "Flatten" the form so it wont be editable/usable anymore
                    //stamper.FormFlattening = true;

                    // Flattens specified Field
                    // stamper.PartialFormFlattening("field1");

                    stamper.Close();
                    pdfReader.Close();
                }
            }
            return RedirectToAction("Index", "Agent");
        }
    }
}