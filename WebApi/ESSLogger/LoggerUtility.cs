using System.Configuration;
using System.Net.Mail;
using System.Text;
using ESSModels.Model;
using Microsoft.Extensions.Options;

namespace ESSLogger
{
    public class LoggerUtility: ILoggerUtility
    {
        private static StreamWriter writer = null;
        private static object lockObj = new object();
        private readonly AppSettings _appSettings;
        private Boolean _isLoggingEnable;
        private Boolean _isInfoLevelLogging;
        private String _logFilePath;
        private String _serverServiceName;
        private Int32 _fileSize;
        public LoggerUtility(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _isLoggingEnable = _appSettings.IsLoggingEnable == "true" ? true : false;
            _isInfoLevelLogging = _appSettings.IsInfoLevelLogging == "true" ? true : false;
            _logFilePath = _appSettings.LogFilePath;
            _serverServiceName = _appSettings.ServerServiceName;
            int iFileSize = 5242880;
            if (!string.IsNullOrEmpty(_appSettings.FileSize))
                Int32.TryParse(_appSettings.FileSize, out iFileSize);
            _fileSize = iFileSize;
        }

        private void PrepareLogger()
        {
            String logFileName = _logFilePath + _serverServiceName + "_"
                                + DateTime.Now.ToUniversalTime().Year.ToString()
                                + "_" + DateTime.Now.ToUniversalTime().Month.ToString()
                                + "_" + DateTime.Now.ToUniversalTime().Day.ToString()
                                + "_Log.txt";
            if (!Directory.Exists(_logFilePath))
            {
                Directory.CreateDirectory(_logFilePath);
            }
            if (File.Exists(logFileName))
            {
                FileInfo fi = new FileInfo(logFileName);
                if (fi.Length > _fileSize)
                {
                    File.Move(logFileName, NewFile());
                    FileStream fs = File.Create(logFileName);
                    fs.Close();
                    fs = null;
                }
            }
            else
            {
                FileStream fs = File.Create(logFileName);
                fs.Close();
                fs = null;
            }
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
                writer = null;
            }
            writer = new StreamWriter(logFileName, true, System.Text.Encoding.Default);
        }

        /// <summary>
        /// Split log file when log file size is greater than 5 mb
        /// </summary>
        /// <returns></returns>
        private string NewFile()
        {
            int index = 0;
            string fileName = _logFilePath + _serverServiceName + "_"
                                + DateTime.Now.ToUniversalTime().Year.ToString() + "_"
                                + DateTime.Now.ToUniversalTime().Month.ToString() + "_"
                                + DateTime.Now.ToUniversalTime().Day.ToString() + "_"
                                + index.ToString() + "_Log.txt";
            while (true)
            {
                if (!File.Exists(fileName))
                {
                    return fileName;
                }
                index++;
                fileName = _logFilePath + _serverServiceName + "_"
                            + DateTime.Now.ToUniversalTime().Year.ToString() + "_"
                            + DateTime.Now.ToUniversalTime().Month.ToString() + "_"
                            + DateTime.Now.ToUniversalTime().Day.ToString() + "_"
                            + index.ToString() + "_Log.txt";
            }
        }


        #region Exception Log
        /// <summary>
        /// Write exception in log file
        /// </summary>
        /// <param name="ex"></param>
        public void WriteExceptionLog(Exception ex)
        {
            try
            {
                if (!_isLoggingEnable)
                    return;
                StringBuilder sbLog = new StringBuilder();
                sbLog.Append("*************************************************************************************************************************************************\r\n");
                sbLog.Append("Type: {0}              CreateTime: {1}\r\nMessage: {2}\r\n\nSource: \r\n{3}\r\n\nDetail: \r\n{4}\r\n\n");
                String item = String.Format(sbLog.ToString(), ErrorLogType.SeverityLevel.Exception.ToString(), System.DateTime.Now.ToUniversalTime().ToString(), ex.ToString(), ex.TargetSite, ex.StackTrace);
                lock (lockObj)
                {
                    PrepareLogger();
                    writer.Write(item);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// Write exception message in log file
        /// </summary>
        /// <param name="message"></param>
        public void WriteExceptionLog(String message)
        {
            try
            {
                if (!_isLoggingEnable)
                    return;
                StringBuilder sbLog = new StringBuilder();
                sbLog.Append("*************************************************************************************************************************************************\r\n");
                sbLog.Append("Type: {0}              CreateTime: {1}\r\nMessage: {2}\r\n");
                String item = String.Format(sbLog.ToString(), ErrorLogType.SeverityLevel.Information.ToString(), System.DateTime.Now.ToUniversalTime().ToString(), message);
                lock (lockObj)
                {
                    PrepareLogger();
                    writer.Write(item);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
            catch { }
        }

        #endregion

        #region Task Log

        /// <summary>
        /// Write message in log file
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="message"></param>
        public void WriteTaskLog(DateTime startDateTime, DateTime endDateTime, String message)
        {
            try
            {
                if (!_isLoggingEnable || !_isInfoLevelLogging)
                    return;
                StringBuilder sbLog = new StringBuilder();
                sbLog.Append("*********************************************************************************************\r\n");
                sbLog.Append("Start: {0}            End: {1}\r\n");
                sbLog.Append("Message: {2}\r\n");
                String item = String.Format(sbLog.ToString(), startDateTime.ToUniversalTime().ToString(), endDateTime.ToUniversalTime().ToString(), message);
                lock (lockObj)
                {
                    PrepareLogger();
                    writer.Write(item);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// Write message in log file
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="message"></param>
        public void WriteLog(DateTime startDateTime, DateTime endDateTime, String message)
        {
            try
            {
                if (!_isLoggingEnable)
                    return;
                StringBuilder sbLog = new StringBuilder();
                sbLog.Append("*********************************************************************************************\r\n");
                sbLog.Append("Start: {0}            End: {1}\r\n");
                sbLog.Append("Message: {2}\r\n");
                String item = String.Format(sbLog.ToString(), startDateTime.ToUniversalTime().ToString(), endDateTime.ToUniversalTime().ToString(), message);
                lock (lockObj)
                {
                    PrepareLogger();
                    writer.Write(item);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// Write message in log file
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="message"></param>
        public void WriteLog(String message)
        {
            try
            {
                if (!_isLoggingEnable)
                    return;
                StringBuilder sbLog = new StringBuilder();
                sbLog.Append("*********************************************************************************************\r\n");
                sbLog.Append("Start: {0}            End: {1}\r\n");
                sbLog.Append("Message: {2}\r\n");
                String item = String.Format(sbLog.ToString(), System.DateTime.Now.ToUniversalTime().ToString(), System.DateTime.Now.ToUniversalTime().ToString(), message);
                lock (lockObj)
                {
                    PrepareLogger();
                    writer.Write(item);
                    writer.Flush();
                    writer.Close();
                    writer = null;
                }
            }
            catch { }
        }

        #endregion

        #region Mail

        public void SendExceptionMail(Exception ex, String mailType, MailErrorLog mailLog, String subject)
        {
            StringBuilder sbBody = new StringBuilder();
            SmtpClient client;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailLog.From);
            mailMessage.To.Add(new MailAddress(mailLog.To));
            mailMessage.Subject = subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            sbBody.AppendLine(GetExceptionData(ex, mailType));
            mailMessage.Body = sbBody.ToString();
            mailMessage.IsBodyHtml = true;
            client = new SmtpClient(mailLog.Server, mailLog.Port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private String GetExceptionData(Exception ex, String mailType)
        {
            StringBuilder sbLog = new StringBuilder();
            sbLog.Append("*************************************************************************************************************************************************\r\n");
            sbLog.Append(String.Format("Type: {0}              CreateTime: {1}\r\nMessage: {2}\r\n\nSource: \r\n{3}\n\nDetail: \r\n{4}\r\n\n", mailType, System.DateTime.Now.ToUniversalTime().ToString(), ex.Message, ex.TargetSite, ex.StackTrace));
            sbLog.Append("*************************************************************************************************************************************************\r\n");
            return sbLog.ToString();
        }

        #endregion
    }
}