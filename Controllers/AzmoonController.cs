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

    public AzmoonController(ILogger<AzmoonController> logger, IOptions<AzmoonetOptions> options, DbContext context, IHubContext<MyHub> hub)
    {
        _logger = logger;
        _options = options.Value;
        _context = context;
        _hub = hub;
    }

    [EnableCors("Policy1")]
    [HttpPost("showNextQuestion")]
    public async Task<IActionResult> getQuestions(QuestionBank_Req request)
    {
        try
        {
            IEnumerable<QuestionBank_Res?> result = await RegistrationBL.getQuestions(request.GroupId, _context);
            await _hub.Clients.All.SendAsync("showNextQuestion", result?.First(p => p?.QuestionNumber == request.CurrentQustionNumber));
            return Ok(new { Message = "Wellcomming message" });
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            return StatusCode(404);
        }

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

    [EnableCors("Policy1")]
    [HttpPost("TakingAnExam")]
    public async Task<ApiResult<StudentRegistration_Res>> TakingAnExam(StudentRegistration_Req request)
    {
        ApiResult<StudentRegistration_Res> result = new ApiResult<StudentRegistration_Res>();
        result.data = new StudentRegistration_Res();
        result.status = 0;
        result.error = 0;
        result.err_description = String.Empty;
        try
        {
            var _result = await RegistrationBL.TakingAnExam(request.MobileNumber, request.ExamId, _context);
            result.data.result = _result;
            result.status = 200;
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            result.data = null;
            result.status = 400;
            result.error = 401;
            result.err_description = e.ToString();
            return result;
        }
    }
}
