using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models.EfModels
{
    [Table("BookingRangeHistory")]
    public class BookingRangeHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int StartHour { get; set; }

        [Required]
        public int StartMinute { get; set; }

        [Required]
        public int EndHour { get; set; }

        [Required]
        public int EndMinute { get; set; }

        [Required]
        public int DaysOfWeekAllowed { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime AppliedDate { get; set; }
    }
}