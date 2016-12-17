using System;

namespace Booking.Web.Helpers
{
    public static class AudienceMapSelector
    {
        public static Guid AudienceMapId
        {
            get
            {
                var str = System.Configuration.ConfigurationManager.AppSettings["AudienceMapId"];
                return str == null ? Guid.Empty : new Guid(str);
            }

            set { System.Configuration.ConfigurationManager.AppSettings["AudienceMapId"] = value.ToString(); }
        }
    }
}