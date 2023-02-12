public class RegisterExamModel_Req
{
    public string MobileNumber { get; set; } = String.Empty;
}

public class RegisterExamModel_Res
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public long ExamId { get; set; }
    public DateTime CreatedAt { get; set; }
}