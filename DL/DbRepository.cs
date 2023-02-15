

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

    public static async Task<IEnumerable<QuestionBank_Res?>> getQuestions(int GroupId, DbContext context)
    {
        string query2 = $@"select * from FROM [exibition_db].[hoseini].[Azmoon_QuestionBank]
        where groupId = {GroupId}";
        try
        {
            var con = context.CreateConnection();
            var result = await con.QueryAsync<QuestionBank_Res>(query2);
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw new NotImplementedException();
        }
    }

    public static async Task<int> TakingAnExam(string SMobile, int examId, DbContext context)
    {
        string query2 = $@"insert into Azmoon_Connection 
        (ExamId,StudentId) 
        values ({SMobile},{examId})";
        try
        {
            var con = context.CreateConnection();
            var result = await con.ExecuteAsync(query2);
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw new NotImplementedException();
        }
    }
}