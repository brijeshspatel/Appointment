using System.ComponentModel.DataAnnotations;

namespace Appointment.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeSlot { get; set; }
        public int Duration { get; set; }

        public string AcceptableTimeSlotCron { get; set; }

        public string ExceptionTimeSlotCron { get; set; }

        //public long StartTimeStamp { get; set; }
    }
}
