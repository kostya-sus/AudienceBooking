using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Booking.Web.Configuration
{
    public class BookingRangeSettings : ConfigurationSection
    {
        private static readonly BookingRangeSettings _bookingRangeSettings =
            ConfigurationManager.GetSection("bookingRangeSettings") as BookingRangeSettings;

        public static BookingRangeSettings Settings
        {
            get { return _bookingRangeSettings; }
        }


        [ConfigurationProperty("rangeStart", IsRequired = true)]
        public DateTime RangeStart
        {
            get { return DateTime.Parse(this["rangeStart"].ToString(), CultureInfo.InvariantCulture); }

            set { this["rangeStart"] = value.ToString("HH:mm"); }
        }

        [ConfigurationProperty("rangeEnd", IsRequired = true)]
        public DateTime RangeEnd
        {
            get { return DateTime.Parse(this["rangeEnd"].ToString(), CultureInfo.InvariantCulture); }

            set { this["rangeEnd"] = value.ToString("HH:mm"); }
        }
    }
}