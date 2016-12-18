using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models.EfModels
{
    [Table("BookingScheduleRule")]
    public class BookingScheduleRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int StartHour { get; set; }

        [Required]
        public int EndHour { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime AppliedDate { get; set; }
    }
}