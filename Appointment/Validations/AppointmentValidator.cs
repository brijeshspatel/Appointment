using Appointment.Validations.Custom;
using FluentValidation;
using Quartz;


namespace Appointment.Validations
{
    public class AppointmentValidator : AbstractValidator<Entities.Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(slot => slot.CronExpression)
                .NotEmpty().WithMessage("Cron expression is required.")
                .Must(BeValidCronExpression).WithMessage("Invalid cron expression.");

            RuleFor(slot => x)
                .Must(BeWithinAcceptableTimeWindow(x.AcceptableTimeSlotCron, x.)).WithMessage("Slot must be between 9 AM and 5 PM.");

            RuleFor(slot => slot.ExceptionTimeSlotCron)
                .Must(NotReservedDuringExceptionPeriod).WithMessage("Slot is reserved during the exception period.");
        }

        private bool BeValidCronExpression(string cronExpression)
        {
            // Validate the cron expression using a library like Quartz.Net or NCrontab.
            // Example: CronExpression.IsValidExpression(cronExpression);
            // Return true if valid, false otherwise.
            // You can use the method CronExpression.ValidateExpression as well.
            // See the Stack Overflow link [^1^][1] for more details.
            return true; // Placeholder; replace with actual validation.
        }

        private bool BeWithinAcceptableTimeWindow(string cronExpression, DateTime value)
        {
            // Extract the time component from the cron expression and check if it falls
            // within the acceptable time window (9 AM to 5 PM).
            // Example: Parse the cron expression and compare the hour/minute.
            //cronExpression = "(0 9 - 16 * **)";

            var schedule = new CronExpression(cronExpression);

            if (schedule.IsSatisfiedBy(value))
            {
                return true;
            }

            return false;
        }

        private bool NotReservedDuringExceptionPeriod(string cronExpression, DateTime value)
        {

        }
    }

    public static class AppointmentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> NotReservedDuringExceptionPeriodIsValid<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotReservedDuringExceptionPeriodValidator<T, TElement>());
        }

        public static IRuleBuilderOptions<T, TElement> BeWithinAcceptableTimeWindowIsValid<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new BeWithinAcceptableTimeWindowValidator<T, TElement>());
        }

    }

}
