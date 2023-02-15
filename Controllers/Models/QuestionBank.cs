
public class QuestionBank_Req
{
    public int GroupId { get; set; }
    public int CurrentQustionNumber { get; set; }
}

public class QuestionBank_Res
{
    public long Id { get; set; }
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; } = String.Empty;
    public string Answer1 { get; set; } = String.Empty;
    public string Answer2 { get; set; } = String.Empty;
    public string Answer3 { get; set; } = String.Empty;
    public int CorrectAnswer { get; set; }

}
