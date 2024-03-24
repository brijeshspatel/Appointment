namespace Appointment.Commands
{
    public interface ICommand<T>
    {
        Task<string> Execute();
    }
}
