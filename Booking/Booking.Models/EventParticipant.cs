namespace Booking.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EventParticipant")]
    public class EventParticipant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        [Required]
        [StringLength(254)]
        public string ParticipantEmail { get; set; }

        public virtual Event Event { get; set; }
    }
}
