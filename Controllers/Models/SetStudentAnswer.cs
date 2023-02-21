public class SetStudentAnswer_req
{
    public string studentId { get; set; } = String.Empty;
    public string examId { get; set; } = String.Empty;
    public string questionId { get; set; } = String.Empty;
    public string correctAnswer { get; set; } = String.Empty;
    public string studentAnswer { get; set; } = String.Empty;
}

public class SetStudentAnswer_res
{
    public bool isInserted { get; set; }
}