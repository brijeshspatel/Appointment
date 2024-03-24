using System.ComponentModel;

namespace Appointment.Entities
{
    public enum ErrorMessages
    {
        [Description("NotReservedDuringExceptionPeriodMustBeValid")]
        NotReservedDuringExceptionPeriodMustBeValid,

        [Description("BeWithinAcceptableTimeWindowMustBeValid")]
        BeWithinAcceptableTimeWindowMustBeValid,

        [Description("Invalid Date and/or Time. Please try again.")]
        InvalidDateTime,

        [Description("Invalid command. Please try again.")]
        InvalidCommand
    }

    public enum Status
    {
        [Description("Success")]
        Success,

        [Description("Error")]
        Error,

        [Description("Exists")]
        Exists
    }
}
