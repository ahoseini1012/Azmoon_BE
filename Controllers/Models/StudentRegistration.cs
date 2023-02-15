public class StudentRegistration_Req
{
    public string MobileNumber { get; set; }=String.Empty;
    public int ExamId { get; set; }
}

public class StudentRegistration_Res
{
    public int result { get; set; }
}