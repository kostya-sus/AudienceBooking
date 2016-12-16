using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models.EfModels
{
    [Table("Audience")]
    public class Audience
    {
        public Audience()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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
        
        [Required]
        public int Left { get; set; }

        [Required]
        public int Top { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        public string RouteImageName { get; set; }

        public string LineDetailsImageName { get; set; }

        [Required]
        [ForeignKey("AudienceMap")]
        public Guid AudienceMapId { get; set; }
        
        public virtual ICollection<Event> Events { get; set; }

        public virtual AudienceMap AudienceMap { get; set; }
    }
}