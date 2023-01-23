namespace ESSModels.Model
{
    public class ErrorLogType
    {
        public enum SeverityLevel
        {
            Warning = 0,
            Error = 1,
            Information = 2,
            Exception = 3,
            Verbose = 4
        }

        public enum LogFileType
        {
            Normal = 0,
            Exception = 1,
            Task = 2
        }
    }
}
