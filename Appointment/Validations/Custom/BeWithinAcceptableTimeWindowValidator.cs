using Appointment.Entities;
using FluentValidation;
using FluentValidation.Validators;
using Quartz;

namespace Appointment.Validations.Custom
{
    public class BeWithinAcceptableTimeWindowValidator<T, TProperty> : IPropertyValidator<T, TProperty>
    {
        public string Name => "BeWithinAcceptableTimeWindowValidator";

        public string GetDefaultMessageTemplate(string errorCode) => ErrorMessages.BeWithinAcceptableTimeWindowMustBeValid.GetDescription();

        public bool IsValid(ValidationContext<T> context, TProperty value)
        {
            // Check if the slot is reserved during the exception period (4 PM to 5 PM
            // on each second day of the third week of any month).
            // Example: Parse the cron expression and check the specific days and hours.
            //(0 9-16 * * *)

            var schedule = new CronExpression((context as Entities.Appointment).ExceptionTimeSlotCron);

            if (schedule.IsSatisfiedBy((context as Entities.Appointment).TimeSlot))
            {
                return true;
            }

            return false;
        }
    }
}
