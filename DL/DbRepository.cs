

using Agricaltech.BL;
using Dapper;

namespace Agricaltech.DL;
public static class DbRepository
{
    public static async Task<IEnumerable<RegisterExamModel_Res>> RegisterExam(string mobileNumber, DbContext context)
    {
        var TMob = Convert.ToInt64(mobileNumber.Substring(mobileNumber.Length - 10, 10));
        string query = $@"
        insert into [Azmoon_Exams]
        ([TeacherId],[CreatedAt],[ExpireAt])
        OUTPUT INSERTED.Id, INSERTED.TeacherId, INSERTED.CreatedAt, INSERTED.ExpireAt
        values ({TMob},GETDATE() ,dateadd(HOUR, 1, getdate()))";


        try
        {
            var con = context.CreateConnection();
            var exam = await con.QueryAsync<RegisterExamModel_Res>(query);
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

    public static async Task<int> StudentLoginToAnExam(string mobilenumber, int examId, DbContext context)
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
        // string checkExamQuery = $@"select * from Azmoon_Exam where id = examId and expireAt>getdate()";

        string query = $@"
        SELECT Top (1) *
        FROM [exibition_db].[hoseini].[Azmoon_Exams]
        where id= {examId} and expireAt>getdate()
        order by CreatedAt desc";
        try
        {
            var con = context.CreateConnection();
            // var checkExam = con.QueryAsync(checkExamQuery);

            var response = await con.QueryAsync<CheckingExam_Res>(query);
            if (response.Count() == 1)
            {
                return response.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw;
        }
    }

    internal static async Task<SetStudentAnswer_res> SetStudentAnswer(SetStudentAnswer_req request, DbContext context)
    {
        SetStudentAnswer_res Results = new SetStudentAnswer_res();
        string query = $@"
            insert into Azmoon_Answers (examId,questionId,correctAnswer,studentId,studentAnswer)
            values ({request.examId},{request.questionId},{request.correctAnswer},{request.studentId},{request.studentAnswer})";
        try
        {
            var con = context.CreateConnection();
            var response = await con.ExecuteAsync(query);
            if (response == 1)
            {
                Results.isInserted = true;
                return Results;
            }
            else
            {
                throw new Exception("خطا در انجام ثبت داده");
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw;
        }
    }
}