using Microsoft.AspNetCore.SignalR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Hubs
{
    public class SignalRPropertyNotifier : IPropertyNotifier
    {
        private readonly IHubContext<PropertyHub> _hubContext;

        public SignalRPropertyNotifier(IHubContext<PropertyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyPropertyAdded(PropertyCreateDto property)
        {
            return _hubContext.Clients.All.SendAsync("PropertyAdded", property);
        }
    }
}
