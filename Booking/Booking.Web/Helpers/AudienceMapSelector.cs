using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Booking.Models.EfModels;
using Booking.Repositories;
using Booking.Repositories.Repositories;
using Booking.Services.Services;

namespace Booking.Web.Helpers
{
    public static class AudienceMapSelector
    {
        private static readonly Configuration Configuration;
        private static readonly string KeyName = "AudienceMapId";

        static AudienceMapSelector()
        {
            Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        }

        public static Guid AudienceMapId
        {
            get
            {
                var setting = Configuration.AppSettings.Settings[KeyName];
                var uof = new UnitOfWork();
                var audienceMapService = new AudienceMapService(uof);


                Guid guid;
                if (setting == null)
                {
                    guid = GetDefaultAudienceMap();
                    Configuration.AppSettings.Settings.Add(KeyName, guid.ToString());
                    Configuration.Save();
                }
                else if (!audienceMapService.Exists(new Guid(setting.Value)))
                {
                    guid = GetDefaultAudienceMap();
                    Configuration.AppSettings.Settings[KeyName].Value = guid.ToString();
                }
                else
                {
                    guid = new Guid(setting.Value);
                }

                return guid;
            }

            set
            {
                Configuration.AppSettings.Settings[KeyName].Value = value.ToString();
                Configuration.Save();
            }
        }

        private static Guid GetDefaultAudienceMap()
        {
            var uof = new UnitOfWork();

            if (!uof.Context.AudienceMaps.Any())
            {
                CreateDefaultAudienceMap();
            }

            return uof.Context.AudienceMaps.First().Id;
        }

        private static void CreateDefaultAudienceMap()
        {
            var uof = new UnitOfWork();
            var audienceMapService = new AudienceMapService(uof);

            var blobFileName = "_default_room_map_generated";
            var filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/walls.png");
            var model = new AudienceMap
            {
                Name = "Default",
                ImageName = blobFileName
            };

            var imageRepository = new ImageBlobRepository();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                imageRepository.UploadImage(fs, blobFileName);
            }

            audienceMapService.CreateAudienceMap(model);
        }
    }
}