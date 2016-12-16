using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;
using Booking.Web.ViewModels;

namespace Booking.Web.Controllers
{
    public class ImageUploadController : Controller
    {
        private IImageRepository _imageRepository = new ImageBlobRepository();

        public ImageUploadController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UploadAsync(ImageUploadVm vm)
        {
            var file = vm.File;
            await _imageRepository.UploadImageAsync(file.InputStream, file.FileName);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public string GetUri(string name)
        {
            return _imageRepository.GetImageUri(name);
        }
    }
}