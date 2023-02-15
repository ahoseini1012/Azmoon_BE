
using Agricaltech.DL;

namespace Agricaltech.BL;
public static class RegistrationBL
{
    public static async Task<IEnumerable<RegisterExamModel_Res>> RegisterExam(string mobileNumber,DbContext context)
    {
        Random random = new Random();
        long examId = random.Next(100000,999999);
        var result = await DbRepository.RegisterExam(mobileNumber,examId.ToString(),context);
        return result;
    }

    public static async Task<int> TakingAnExam(string SMobile , int examId,DbContext context)
    {
        var result = await DbRepository.TakingAnExam(SMobile,examId,context);
        return result;
    }

    public static async Task<IEnumerable<QuestionBank_Res?>> getQuestions(int GroupId,DbContext context)
    {
        var result = await DbRepository.getQuestions(GroupId,context);
        return result;
    }

}