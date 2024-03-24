using Appointment.Services.Interfaces;

namespace Appointment.Commands
{
    class AddCommand<T> : ICommand<T>
    {
        private readonly IService<T> _typeService;
        private readonly T _type;

        public AddCommand(IService<T> typeService, T type)
        {
            _typeService = typeService;
            _type = type;
        }
        public async Task<string> Execute()
        {
            return await (_typeService?.Add(_type));
        }
    }
}
