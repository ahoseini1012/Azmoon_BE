

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
        string query2 = $@"select * from [exibition_db].[hoseini].[Azmoon_QuestionBank]
        where QuestionGroupId = {GroupId}";
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

    public static async Task<int> TakingAnExam(string mobilenumber , int examId, DbContext context)
    {
        string query = $@"insert into Azmoon_Connection 
        (mobileNumber , ExamId) 
        values ({mobilenumber},{examId})";
        try
        {
            var con = context.CreateConnection();
            var result = await con.ExecuteAsync(query);
            return result;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw;
        }
    }

    public static async Task<CheckingExam_Res?> CheckingExam(int examId, DbContext context)
    {
        string query = $@"
        SELECT Top (1) *
        FROM [exibition_db].[hoseini].[Azmoon_Exams]
        where ExamId= {examId}
        order by CreatedAt desc";
        try
        {
            var con = context.CreateConnection();
            var response = await con.QueryAsync<CheckingExam_Res>(query);
            if (response.Count() == 1)
            {
                return response.FirstOrDefault();
            }else{
                return null ;
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw;
        }
    }
}