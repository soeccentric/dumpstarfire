using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
  public class FileUploadController : AsyncController
  {
    // GET: FileUpload
    [HttpPost]
    public async Task<ActionResult> Index(HttpPostedFileBase file)
    {
      var fileData = new byte[file.ContentLength];
      await file.InputStream.ReadAsync(fileData, 0, file.ContentLength);
      //read this in as byte[]
      var poop = await Task.FromResult(false);
      return new JsonResult() { Data = poop };
    }
  }
}