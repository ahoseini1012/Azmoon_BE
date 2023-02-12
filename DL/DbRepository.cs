

using Agricaltech.BL;
using Dapper;

namespace Agricaltech.DL;
public static class DbRepository
{
    public static async Task<IEnumerable<RegisterExamModel_Res>> RegisterExam(string mobileNumber, string examId, DbContext context)
    {
        string query2 = $@"insert into Azmoon_Exams 
        (TeacherId,ExamId,CreatedAt) 
        OUTPUT INSERTED.Id, INSERTED.TeacherId, INSERTED.ExamId, INSERTED.CreatedAt
        values ({mobileNumber},{examId},GETDATE() )";
        try
        {
            var con = context.CreateConnection();
            var exam = await con.QueryAsync<RegisterExamModel_Res>(query2);
            return exam;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw new NotImplementedException();
        }
    }
}