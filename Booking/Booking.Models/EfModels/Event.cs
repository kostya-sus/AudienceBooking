using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models.EfModels
{
    [Table("Event")]
    public class Event
    {
        public Event()
        {
            EventParticipants = new HashSet<EventParticipant>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }
        
        [StringLength(50)]
        public string Title { get; set; }
        
        [StringLength(128)]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public bool IsJoinAvailable { get; set; }
        
        [Required]
        [ForeignKey("Audience")]
        public Guid AudienceId { get; set; }

        [Required]
        public bool IsAuthorShown { get; set; }

        [StringLength(30)]
        public string AuthorName { get; set; }

        [StringLength(600)]
        public string AdditionalInfo { get; set; }

        public virtual Audience Audience { get; set; }

        public virtual ICollection<EventParticipant> EventParticipants { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}