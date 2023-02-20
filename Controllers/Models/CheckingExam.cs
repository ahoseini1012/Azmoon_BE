public class CheckingExam_Req
{
    public int ExamId { get; set; }
}

public class CheckingExam_Res
{
    public long Id { get; set; }
    public string TeacherId { get; set; }=String.Empty;
    public long ExamId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
}