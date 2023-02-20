
public class QuestionBank_Req
{
    public int GroupId { get; set; }
    public string HubGroupName { get; set; }= String.Empty;
    public int CurrentQustionNumber { get; set; }
    public int AddQuestionNumber { get; set; }
}

public class QuestionBank_Res
{
    public long Id { get; set; }
    public int QuestionNumber { get; set; }
    public string Question { get; set; } = String.Empty;
    public string Answer1 { get; set; } = String.Empty;
    public string Answer2 { get; set; } = String.Empty;
    public string Answer3 { get; set; } = String.Empty;
}
