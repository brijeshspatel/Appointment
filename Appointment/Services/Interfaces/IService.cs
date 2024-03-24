namespace Appointment.Services.Interfaces
{
    public interface IService<T>
    {
        Task<string> Add<T>(T? type);
    }
}
