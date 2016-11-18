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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public Rooms AudienceId { get; set; }

        public bool IsAuthorShown { get; set; }

        [StringLength(30)]
        public string AuthorName { get; set; }

        [StringLength(600)]
        public string AdditionalInfo { get; set; }

        public virtual Audience Audience { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
