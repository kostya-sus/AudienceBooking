using Booking.Enums;

namespace Booking.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Audience")]
    public class Audience
    {
        public Audience()
        {
            Events = new HashSet<Event>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Audiences Id { get; set; }

        public string Name { get; set; }

        public int SeatsCount { get; set; }

        public int BoardsCount { get; set; }

        public int LaptopsCount { get; set; }

        public int PrintersCount { get; set; }

        public int ProjectorsCount { get; set; }

        public bool IsBookingAvailable { get; set; }
        
        public ICollection<Event> Events { get; set; }
    }
}
