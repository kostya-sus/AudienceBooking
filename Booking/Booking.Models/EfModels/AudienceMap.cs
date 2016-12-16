using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models.EfModels
{
    [Table("AudienceMap")]
    public class AudienceMap
    {
        public AudienceMap()
        {
            Audiences = new HashSet<Audience>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageName { get; set; }

        public virtual ICollection<Audience> Audiences { get; set; }
    }
}