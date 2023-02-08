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




    private readonly List<QuestionDetail> _data = new List<QuestionDetail>();

    public AzmoonController(ILogger<AzmoonController> logger, IOptions<AzmoonetOptions> options, DbContext context, IHubContext<MyHub> hub)
    {
        _logger = logger;
        _options = options.Value;
        _context = context;
        _hub = hub;

        for (int i = 1; i < 6; i++)
        {
            _data.Add(new QuestionDetail
            {
                questionNumber = i,
                questionId = 1000+i,
                question = string.Format("سوال شماره {0}", i.ToString()),
                responses =  new List<string>(){"گزینه الف","گزینه ب","هیچکدام"}
            });
        }
    }

    [HttpPost("showNextQuestion")]
    public IActionResult changeQuestion(NextQuestionBodyRequest request)
    {
        _hub.Clients.All.SendAsync("showNextQuestion", _data[request.currentQuestionNumber + request.addQuestionNumber - 1]);
        return Ok(new { Message = "Wellcomming message" });
    }

    [HttpPost("setStudentAnswer")]
    public IActionResult setStudentAnswer(ClientAnswer request)
    {
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
