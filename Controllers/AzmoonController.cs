using Agricaltech.BL;
using Agricaltech.DL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Agricaltech.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AzmoonController : ControllerBase
{
    private readonly ILogger<AzmoonController> _logger;
    private readonly AzmoonetOptions _options;
    private readonly DbContext _context;
    private readonly IHubContext<MyHub> _hub;

    private class data
    {
        public int questionNumber { get; set; }
        public string question { get; set; } = String.Empty;
    }

    public class NextQuestionBodyRequest
    {
        public string mobileNumber { get; set; }=String.Empty;
        public int currentQuestionNumber { get; set; }
    }
    private readonly List<data> _data = new List<data>();

    public AzmoonController(ILogger<AzmoonController> logger, IOptions<AzmoonetOptions> options, DbContext context, IHubContext<MyHub> hub)
    {
        _logger = logger;
        _options = options.Value;
        _context = context;
        _hub = hub;

        for (int i = 1; i < 6; i++)
        {
            _data.Add(new data
            {
                questionNumber = i,
                question = string.Format("سوال شماره {0}", i.ToString())
            });
        }
    }

    [HttpPost("showNextQuestion")]
    public IActionResult showNextQuestion(NextQuestionBodyRequest request)
    {
        _hub.Clients.All.SendAsync("showNextQuestion", _data[request.currentQuestionNumber]);
        return Ok(new { Message = "Wellcomming message" });
    }


    // [HttpPost("GetAttendeeByMobile")]
    // public async Task<IEnumerable<Attendee>> GetAttendeeInfo(GetAttendeeByMobileModel request)
    // {
    //     var config = _options.ConnectionString;
    //     IEnumerable<Attendee> attendees = await RegistrationBL.GetAttendeeByMobile(request.MobileNo, _context);
    //     return attendees;
    // }
}
