using Booking.Enums;

namespace Booking.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Audience")]
    public class Audience
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Audience()
        {
            Events = new HashSet<Event>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Rooms Id { get; set; }

        public string Name { get; set; }

        public int SeatsCount { get; set; }

        public int BoardsCount { get; set; }

        public int LaptopsCount { get; set; }

        public int PrintersCount { get; set; }

        public int ProjectorsCount { get; set; }

        public bool IsBookingAvailable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
