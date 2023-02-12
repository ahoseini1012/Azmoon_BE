using Agricaltech.BL;
using Agricaltech.DL;
using Microsoft.AspNetCore.Cors;
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
                questionId = 1000 + i,
                question = string.Format("سوال شماره {0}", i.ToString()),
                responses = new List<string>() { "گزینه الف", "گزینه ب", "هیچکدام" }
            });
        }
    }

    [EnableCors("Policy1")]
    [HttpPost("showNextQuestion")]
    public IActionResult changeQuestion(NextQuestionBodyRequest request)
    {
        _hub.Clients.All.SendAsync("showNextQuestion", _data[request.currentQuestionNumber + request.addQuestionNumber - 1]);
        return Ok(new { Message = "Wellcomming message" });
    }
    [EnableCors("Policy1")]
    [HttpPost("setStudentAnswer")]
    public IActionResult setStudentAnswer(ClientAnswer request)
    {
        return Ok(new { Message = "Wellcomming message" });
    }

    [EnableCors("Policy1")]
    [HttpPost("RegisterExam")]
    public async Task<ApiResult<RegisterExamModel_Res>> RegisterExam(RegisterExamModel_Req request)
    {
        ApiResult<RegisterExamModel_Res> result = new ApiResult<RegisterExamModel_Res>();
        try
        {
            var config = _options.ConnectionString;
            IEnumerable<RegisterExamModel_Res> registerExamModel = await RegistrationBL.RegisterExam(request.MobileNumber, _context);
            result.data = registerExamModel.First();
            result.status = 200;
            result.error = -1;
            result.err_description = String.Empty;
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            result.data!.Id = -1;
            result.data.TeacherId = -1;
            result.data.ExamId = -1;
            result.status = 200;
            result.error = -1;
            result.err_description = String.Empty;
            return result;
        }
    }
}
