using APIGateway.SignalRHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.SignalRModels;

namespace APIGateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationsController(IHubContext<NotificationsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel model)
        {
            await _hubContext.Clients.Group(model.UserId).SendAsync("ReceiveNotification", model.Message);
            return Ok();
        }
    }
}
