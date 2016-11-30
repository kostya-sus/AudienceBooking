using System.ComponentModel.DataAnnotations;
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public AudiencesEnum Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SeatsCount { get; set; }

        [Required]
        public int BoardsCount { get; set; }

        [Required]
        public int LaptopsCount { get; set; }

        [Required]
        public int PrintersCount { get; set; }

        [Required]
        public int ProjectorsCount { get; set; }

        [Required]
        public bool IsBookingAvailable { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}