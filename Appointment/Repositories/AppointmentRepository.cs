
using Appointment.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Entities.Appointment?> GetByStartDateTimeAsync(DateTime startDateTime)
        {
            var Appointment = await _context.Appointments
               .FirstOrDefaultAsync(m => m.TimeSlot == startDateTime);

            return Appointment;
        }

        public async Task InsertAsync(Entities.Appointment Appointment)
        {
            await _context.Appointments.AddAsync(Appointment);
        }

        public async Task UpdateAsync(Entities.Appointment Appointment)
        {
            _context.Appointments.Update(Appointment);
        }

        public async Task DeleteAsync(int AppointmentId)
        {
            var Appointment = await _context.Appointments.FindAsync(AppointmentId);
            if (Appointment != null)
            {
                _context.Appointments.Remove(Appointment);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}