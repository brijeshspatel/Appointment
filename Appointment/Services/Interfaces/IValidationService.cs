namespace Appointment.Services.Interfaces
{
    public interface IValidationService
    {
        bool IsValidInput(string input);

        bool IsValidDate(string startDateTime);
    }
}
