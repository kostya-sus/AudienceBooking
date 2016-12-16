using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Booking.Models.EfModels;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Repositories.Repositories;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.ViewModels.AdminPanel;

namespace Booking.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private IAudienceMapService _audienceMapService;
        private IImageRepository _imageRepository = new ImageBlobRepository();

        public AdminPanelController()
        {
            var uof = new UnitOfWork();
            _audienceMapService = new AudienceMapService(uof);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var vm = new AdminPanelViewModel
            {
                AudienceMaps = _audienceMapService.GetAllAudienceMaps().ToDictionary(x => x.Id, x => x.Name)
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult CreateAudienceMapPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAudienceMap(NewAudienceMapViewModel vm)
        {
            await _imageRepository.UploadImageAsync(vm.Image.InputStream, vm.Image.FileName);
            var model = new AudienceMap
            {
                Name = vm.Name,
                ImageName = vm.Image.FileName
            };

            _audienceMapService.CreateAudienceMap(model);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAudienceMap(Guid id)
        {
            var audienceMap = _audienceMapService.GetAudienceMap(id);
            _audienceMapService.DeleteAudienceMap(audienceMap);
            return RedirectToAction("Index");
        }
    }
}