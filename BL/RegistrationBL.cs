
using Agricaltech.DL;

namespace Agricaltech.BL;
public static class RegistrationBL
{
    public static async Task<IEnumerable<RegisterExamModel_Res>> RegisterExam(string mobileNumber, DbContext context)
    {
        var result = await DbRepository.RegisterExam(mobileNumber, context);
        return result;
    }

    public static async Task<int> StudentLoginToAnExam(string mobileNumber, int examId, DbContext context)
    {
        var result = await DbRepository.StudentLoginToAnExam(mobileNumber, examId, context);
        return result;
    }

    public static async Task<IEnumerable<QuestionBank_Res?>> getQuestions(int GroupId, DbContext context)
    {
        var result = await DbRepository.getQuestions(GroupId, context);
        return result;
    }

    public static async Task<CheckingExam_Res?> CheckingExam(int examId, DbContext context)
    {
        var result = await DbRepository.CheckingExam(examId, context);
        return result;
    }

    public static async Task<SetStudentAnswer_res> SetStudentAnswer(SetStudentAnswer_req request, DbContext context)
    {
        var result = await DbRepository.SetStudentAnswer(request, context);
        return result;
    }
}