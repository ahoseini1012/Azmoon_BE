public class ApiResult<T>
{
    public int status { get; set; }
    public T? data { get; set; }
    public int error { get; set; }
    public string err_description { get; set; } = String.Empty;
}