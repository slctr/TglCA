namespace TglCA.Bll.Interfaces.Entities
{
    public class ErrorDetail
    {
        public string ErrorMessage { get; set; }

        public Exception? Exception { get; set; }
    }
}
