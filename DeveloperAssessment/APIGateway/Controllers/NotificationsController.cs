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
        private readonly IConfiguration _config;
        public NotificationsController(IHubContext<NotificationsHub> hubContext, IConfiguration config)
        {
            _hubContext = hubContext;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel model)
        {
            var signalrMethodName = _config.GetSection("SignalR")["MethodName"];
            await _hubContext.Clients.Group(model.UserId).SendAsync(signalrMethodName, model.Message);
            return Ok();
        }
    }
}
