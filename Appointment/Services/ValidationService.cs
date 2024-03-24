using System.Globalization;
using System.Text.RegularExpressions;
using Appointment.Services.Interfaces;

namespace Appointment.Services
{
    public class ValidationService : IValidationService
    {
        public bool IsValidInput(string input)
        {
            string addPattern = @"^ADD (\d{2}/\d{2} \d{2}:\d{2})$";
            string deletePattern = @"^DELETE (\d{2}/\d{2} \d{2}:\d{2})$";
            string findPattern = @"^FIND (\d{2}/\d{2})$";
            string keepPattern = @"^KEEP (\d{2}:\d{2})$";
            string exitPattern = @"^EXIT$";

            if (!string.IsNullOrEmpty(input))
            {
                input = input.Trim().ToUpper();


                if ((Regex.IsMatch(input, addPattern)) || (Regex.IsMatch(input, deletePattern)) || (Regex.IsMatch(input, findPattern)) || (Regex.IsMatch(input, keepPattern)) || (Regex.IsMatch(input, exitPattern)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool IsValidDate(string dateTime)
        {
            string format = "dd/MM/yyyy HH:mm:ss"; // Expected format

            if (DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
