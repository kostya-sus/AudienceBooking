using System;
using System.Configuration;

namespace Booking.Web.Helpers
{
    public static class AudienceMapSelector
    {
        private static Configuration _configuration;
        private static readonly string KeyName = "AudienceMapId";

        static AudienceMapSelector()
        {
            _configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            if (_configuration.AppSettings.Settings[KeyName] == null)
            {
                _configuration.AppSettings.Settings.Add(KeyName, Guid.Empty.ToString());
                _configuration.Save();
            }
        }

        public static Guid AudienceMapId
        {
            get
            {
                var str = _configuration.AppSettings.Settings[KeyName].Value;
                return new Guid(str);
            }

            set
            {
                _configuration.AppSettings.Settings[KeyName].Value = value.ToString();
                _configuration.Save();
            }
        }
    }
}