using Appointment.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Appointment.Services
{
    public class AppointmentEngine : IAppointmentEngine
    {
        private readonly ILogger<AppointmentEngine> _logger;

        private readonly string _environment;
        private readonly IConfiguration _config;

        public AppointmentEngine(ILogger<AppointmentEngine> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            //_environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public void Run()
        {
            _logger.LogInformation($"Running AppointmentEngire {_environment}");
            _logger.LogInformation("Initialized application in {Env} to run migration", _environment);

            bool exitApp = false;
            while (!exitApp)
            {
                Console.Write("Enter command: ");

                string input = Console.ReadLine();


                if (!string.IsNullOrEmpty(input))
                {
                    string[] command = input.Trim().ToUpper().Split(' ');

                    string appointmentDateTime = $"{command[1]}/{DateTime.Now.Year} {command[2]}:00";

                    {
                        switch (command[0])
                        {
                            case "ADD":
                                var appointment = new Entities.Appointment
                                {
                                    TimeSlot = DateTime.Parse(appointmentDateTime),
                                    Duration = Convert.ToInt32(_config["Duration"]),
                                    AcceptableTimeSlotCron = _config["AcceptableTimeSlotCron"],
                                    ExceptionTimeSlotCron = _config["ExceptionTimeSlotCron"]
                                };


                                var status = appointmentService?.Add(appointment);

                                Console.WriteLine($"The appointment has been succesfully added.");

                                break;

                            case "DELETE":
                                appointmentService?.Delete(DateTime.Parse(appointmentDateTime));
                                Console.WriteLine($"The appointment has been succesfully removed.");
                                break;

                            case "FIND":
                                var freeSlot = appointmentService?.Find(DateTime.Parse(appointmentDateTime));
                                Console.WriteLine($"Free slot: {freeSlot}");
                                break;

                            case "KEEP":
                                appointmentService?.Keep(TimeSpan.Parse(appointmentDateTime));
                                Console.WriteLine($"The appointment has been succesfully kept.");
                                break;

                            case "EXIT":
                                exitApp = true;
                                break;
                        }
                    }
                }
            }
        }

        static void DisplayIntroduction()
        {
            Console.WriteLine("Welcome to the TAL Appointment App!");
            Console.WriteLine("Use the following commands to interact with the application:");
            Console.WriteLine(" - 'ADD DD/MM hh:mm' to add an appointment.");
            Console.WriteLine(" - 'DELETE DD/MM hh:mm' to remove an appointment.");
            Console.WriteLine(" - 'FIND DD/MM' to find a free timeslot for the day.");
            Console.WriteLine(" - 'KEEP hh:mm' to keep a timeslot for any day.");
            Console.WriteLine(" - 'EXIT' to close the application.\n\n");
        }

    }
}