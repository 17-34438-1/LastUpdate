using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace work_01.Controllers
{
    public class FileManagerController : Controller
    {
        private readonly IHostingEnvironment _hostEnv;
        public FileManagerController(IHostingEnvironment hostEnv)
        {
            this._hostEnv = hostEnv;
        }
        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile filetoupload)
        {
            string msg = "";
            if(filetoupload!=null && filetoupload.Length > 0)
            {
                if (filetoupload.Length < 2 * 1024 * 1024)
                {
                    string ext = Path.GetExtension(filetoupload.FileName);
                    if(ext==".jpg" || ext==".png" || ext == ".gif")
                    {
                        string webroot = _hostEnv.WebRootPath;
                        string folder = "UploadedFiles";
                        //string filename = Guid.NewGuid()+Path.GetFileName(filetoupload.FileName);
                        string filename = DateTime.Now.ToString("yyyy_MMM_dd_hh_mm_ss") + Path.GetFileName(filetoupload.FileName);
                        string filetowrite = Path.Combine(webroot, folder, filename);

                        using (var stream = new FileStream(filetowrite, FileMode.Create))
                        {
                            await filetoupload.CopyToAsync(stream);
                            msg = "File [" + filename + "] is uploaded successfully!!";
                        }
                    }
                    else
                    {
                        msg = "Only jpg, gif or png file allowed.";
                    }
                  
                }
                else
                {
                    msg = "File size exceeds 2mb limit.";
                }
                

            }
            else
            {
                msg = "Please select a valid file to upload.";
            }
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }
    }
}