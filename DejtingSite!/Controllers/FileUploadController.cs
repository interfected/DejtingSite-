using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejtingSidan.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        var existingFile = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles")).
                            Where(f => f.Contains(User.Identity.GetUserId()));
                        if(existingFile.Count() > 0)
                        {
                            foreach(var existing in existingFile)
                            {
                                System.IO.File.Delete(existing);
                            }
                        }
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), UserManager.FindById(User.Identity.GetUserId()).Id += Path.GetExtension(file.FileName));
                        file.SaveAs(path);
                    }
                    ViewBag.FileStatus = "Filen har laddats upp.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Fel uppkom när filen laddades upp";
                }
            }
            return RedirectToAction("Edit", "Account");
        }
    }
}