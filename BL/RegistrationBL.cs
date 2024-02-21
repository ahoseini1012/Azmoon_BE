
using Agricaltech.DL;

namespace Agricaltech.BL;
public static class RegistrationBL
{
    public static async Task<IEnumerable<RegisterExamModel_Res>> RegisterExam(string mobileNumber, DbContext context,ILogger _logger)
    {
        var result = await DbRepository.RegisterExam(mobileNumber, context,_logger);
        return result;
    }

    public static async Task<int> StudentLoginToAnExam(string mobileNumber, int examId, DbContext context,ILogger _logger)
    {
        var result = await DbRepository.StudentLoginToAnExam(mobileNumber, examId, context,_logger);
        return result;
    }

    public static async Task<IEnumerable<QuestionBank_Res?>> getQuestions(int nextQuestoinNumber, DbContext context,ILogger _logger)
    {
        var result = await DbRepository.getQuestions(nextQuestoinNumber, context,_logger);
        return result;
    }

    public static async Task<CheckingExam_Res?> CheckingExam(int examId, DbContext context,ILogger _logger)
    {
        var result = await DbRepository.CheckingExam(examId, context,_logger);
        return result;
    }

    public static async Task<SetStudentAnswer_res> SetStudentAnswer(SetStudentAnswer_req request, DbContext context,ILogger _logger)
    {
        _logger.LogInformation("BL:SetStudentAnswer:");
        var result = await DbRepository.SetStudentAnswer(request, context,_logger);
        return result;
    }

        public static async Task<IEnumerable<ReposrtAnswers_res>> ReportAnswers(ReposrtAnswers_req request, DbContext context,ILogger _logger)
    {
        _logger.LogInformation("BL:SetStudentAnswer:");
        var result = await DbRepository.ReportAnswers(request, context,_logger);
        return result;
    }
}