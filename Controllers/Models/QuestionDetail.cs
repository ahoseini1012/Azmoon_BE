    public class QuestionDetail
    {
        public int questionNumber { get; set; }
        public long questionId { get; set; }
        public string question { get; set; } = String.Empty;
        public List<string>? responses { get; set; }
    }