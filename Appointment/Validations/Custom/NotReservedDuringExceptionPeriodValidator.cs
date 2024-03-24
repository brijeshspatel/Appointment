using FluentValidation.Validators;
using FluentValidation;
using Appointment.Entities;
using Quartz;

namespace Appointment.Validations.Custom
{
    public class NotReservedDuringExceptionPeriodValidator<T, TProperty> : IPropertyValidator<T, TProperty>
    {
        public string Name => "NotReservedDuringExceptionPeriodValidator";

        public string GetDefaultMessageTemplate(string errorCode) => ErrorMessages.NotReservedDuringExceptionPeriodMustBeValid.GetDescription();

        public bool IsValid(ValidationContext<T> context, TProperty value)
        {
            // Check if the slot is reserved during the exception period (4 PM to 5 PM
            // on each second day of the third week of any month).
            // Example: Parse the cron expression and check the specific days and hours.
            //(0 9 - 15,17 - 23 8 - 14,22 - 28 * *)

            var schedule = new CronExpression((context as Entities.Appointment).ExceptionTimeSlotCron);

            if (schedule.IsSatisfiedBy((context as Entities.Appointment).TimeSlot))
            {
                return true;
            }

            return false;
        }
    }
}
