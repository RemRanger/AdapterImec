namespace AdapterImec.Api.Models
{
    public class IcarError
    {
        public IcarError(string type, string severity, int status, string title, string detail, string instance)
        {
            Type = type;
            Severity = severity;
            Status = status;
            Title = title;
            Detail = detail;
            Instance = instance;
        }

        public string Type { get; }
        public string Severity { get; }
        public int Status { get; }
        public string Title { get; }
        public string Detail { get; }
        public string Instance { get; }
    }

    public class IcarErrors
    {
        public IcarErrors(IcarError error)
        {
            this.Errors = new IcarError[] { error };
        }

        public IcarError[] Errors { get; }
    }
}
