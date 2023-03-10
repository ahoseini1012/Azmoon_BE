using Agricaltech.BL;
using Agricaltech.DL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
    public async Task<IActionResult> showNextQuestion(QuestionBank_Req request)
    {
        _logger.LogInformation("controller: " + JsonConvert.SerializeObject(request));
        try
        {
            var nextQuestoinNumber = request.CurrentQuestionNumber + request.AddQuestionNumber;
            IEnumerable<QuestionBank_Res?> result = await RegistrationBL.getQuestions(request.GroupId, nextQuestoinNumber , _context,_logger);
            var data = result?.First(p => p?.QuestionNumber == request.CurrentQuestionNumber + request.AddQuestionNumber);
            _logger.LogInformation("controller: "+JsonConvert.SerializeObject(data));
            await _hub.Clients.All.SendAsync("showNextQuestion", data);
            // await _hub.Clients.Group(request.HubGroupName).SendAsync("showNextQuestion",data);
            return Ok(new { Message = "1" });
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            return StatusCode(404);
        }
    }



    [EnableCors("Policy1")]
    [HttpPost("SetStudentAnswer")]
    public async Task<ApiResult<SetStudentAnswer_res>> SetStudentAnswer(SetStudentAnswer_req request)
    {
        _logger.LogInformation("controller:SetStudentAnswer:  " + JsonConvert.SerializeObject(request));
        ApiResult<SetStudentAnswer_res> result = new ApiResult<SetStudentAnswer_res>();

        try
        {
            SetStudentAnswer_res data = await RegistrationBL.SetStudentAnswer(request, _context, _logger);
            _logger.LogInformation("controller:SetStudentAnswer:  " + JsonConvert.SerializeObject(data));

            if (data.isInserted)
            {
                result.data = data;
                result.status = 200;
                result.error = -1;
                result.err_description = String.Empty;
                return result;
            }
            else
            {
                throw new Exception("?????? ?????????? ????????????");
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            result.data = null;
            result.status = 400;
            result.error = -1;
            result.err_description = String.Empty;
            return result;
        }

    }

    [EnableCors("Policy1")]
    [HttpPost("RegisterExam")]
    public async Task<ApiResult<RegisterExamModel_Res>> RegisterExam(RegisterExamModel_Req request)
    {
        _logger.LogInformation("controller: " + JsonConvert.SerializeObject(request));
        ApiResult<RegisterExamModel_Res> result = new ApiResult<RegisterExamModel_Res>();
        try
        {
            IEnumerable<RegisterExamModel_Res> data = await RegistrationBL.RegisterExam(request.MobileNumber, _context, _logger);
            _logger.LogInformation("controller: " + JsonConvert.SerializeObject(data));

            result.data = data.First();
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
    [HttpPost("CheckingExam")]
    public async Task<ApiResult<CheckingExam_Res>> CheckingExam(CheckingExam_Req request)
    {
        _logger.LogInformation("controller: " + JsonConvert.SerializeObject(request));

        ApiResult<CheckingExam_Res> result = new ApiResult<CheckingExam_Res>();
        result.data = new CheckingExam_Res();
        result.status = 0;
        result.error = 0;
        result.err_description = String.Empty;
        try
        {
            var _result = await RegistrationBL.CheckingExam(request.ExamId, _context, _logger);
            _logger.LogInformation("controller: " + JsonConvert.SerializeObject(_result));

            if (_result == null)
            {
                throw new System.Exception("?????????? ??????????????");
            }

            result.data = _result;
            result.status = 200;
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            result.data = null;
            result.status = 400;
            result.error = 401;
            result.err_description = e.Message;
            return result;
        }
    }

    [EnableCors("Policy1")]
    [HttpPost("StudentLoginToAnExam")]
    public async Task<ApiResult<StudentRegistration_Res>> StudentLoginToAnExam(StudentRegistration_Req request)
    {
        _logger.LogInformation("controller: " + JsonConvert.SerializeObject(request));
        ApiResult<StudentRegistration_Res> result = new ApiResult<StudentRegistration_Res>();
        result.data = new StudentRegistration_Res();
        result.status = 0;
        result.error = 0;
        result.err_description = String.Empty;
        try
        {
            var _result = await RegistrationBL.StudentLoginToAnExam(request.MobileNumber, request.ExamId, _context, _logger);
            _logger.LogInformation("controller: " + JsonConvert.SerializeObject(_result));

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

    [EnableCors("Policy1")]
    [HttpPost("ReportAnswers")]
    public async Task<ApiResult<IEnumerable<ReposrtAnswers_res>>> ReportAnswers(ReposrtAnswers_req request)
    {
        _logger.LogInformation("controller:ReportAnswers:  " + JsonConvert.SerializeObject(request));
        ApiResult<IEnumerable<ReposrtAnswers_res>> result = new ApiResult<IEnumerable<ReposrtAnswers_res>>();
        result.data = null;
        result.status = 0;
        result.error = 0;
        result.err_description = String.Empty;
        try
        {
            var _result = await RegistrationBL.ReportAnswers(request , _context, _logger);
            result.data = _result;
            result.status = 200;
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
            result.status = 400;
            result.error = 401;
            result.err_description = e.ToString();
            return result;
        }
    }
}
