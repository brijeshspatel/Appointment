namespace Appointment.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Entities.Appointment>> GetAllAsync();

        Task<Entities.Appointment?> GetByStartDateTimeAsync(DateTime startDateTime);

        Task InsertAsync(Entities.Appointment Appointment);

        Task UpdateAsync(Entities.Appointment Appointment);

        Task DeleteAsync(int AppointmentId);

        Task SaveAsync();
    }
}
