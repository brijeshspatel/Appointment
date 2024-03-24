using Appointment.Data;
using Appointment.Entities;
using Appointment.Repositories;
using Microsoft.Extensions.Configuration;
using Appointment.Services.Interfaces;

namespace Appointment.Services
{
    public class AppointmentService : IService<Entities.Appointment>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConfiguration _config;

        public AppointmentService(IAppointmentRepository appointmentRepository, ApplicationDbContext context, IConfiguration config)
        {
            _appointmentRepository = appointmentRepository;
            _context = context;
            _config = config;
        }

        public async Task<string> Add(Entities.Appointment? appointment)
        {
            try
            {
                var existingappointment = await _appointmentRepository.GetByStartDateTimeAsync(appointment.TimeSlot);

                if (existingappointment == null)
                {
                    await _appointmentRepository.InsertAsync(appointment);

                    await _appointmentRepository.SaveAsync();

                    return Status.Success.GetDescription();
                }
                else
                {
                    return Status.Exists.GetDescription();
                }
            }
            catch
            {
                return Status.Error.GetDescription();
            }
        }

        public Task<string> Add<T>(T? type)
        {
            throw new NotImplementedException();
        }

        //public void Delete(DateTime dateTime)
        //{
        //    throw new NotImplementedException();
        //}

        //public DateTime Find(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Keep(TimeSpan time)
        //{
        //    throw new NotImplementedException();
        //}

        //private bool GetByStartDateTime(DateTime dateTime)
        //{
        //    //var appointment = await _appointmentRepository.GetByStartDateTimeAsync(dateTime);

        //    //using (var context = new ApplicationDbContext())
        //    //{
        //    //    var appointment = new Appointment { StartDateTime = dateTime, EndDateTime = dateTime.AddMinutes(30) };
        //    //    context.Appointments.Add(appointment);
        //    //    context.SaveChanges();
        //    //}

        //    return false;
        //}
    }
}
