using Booking.Enums;

namespace Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Event")]
    public class Event
    {
        public Event()
        {
            EventParticipants = new HashSet<EventParticipant>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime EventDateTime { get; set; }

        public int Duration { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(128)]
        public string AuthorId { get; set; }

        public bool IsPublic { get; set; }

        public bool IsJoinAvailable { get; set; }

        public Audiences AudienceId { get; set; }

        public bool IsAuthorShown { get; set; }

        [StringLength(30)]
        public string AuthorName { get; set; }

        [StringLength(600)]
        public string AdditionalInfo { get; set; }

        public virtual Audience Audience { get; set; }
        
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
