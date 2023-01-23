using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESSModels.Model;

namespace ESSLogger
{
    public interface ILoggerUtility
    {
        void WriteExceptionLog(Exception ex);
        void WriteExceptionLog(String message);
        void WriteTaskLog(DateTime startDateTime, DateTime endDateTime, String message);
        void WriteLog(DateTime startDateTime, DateTime endDateTime, String message);
        void WriteLog(String message);
        void SendExceptionMail(Exception ex, String mailType, MailErrorLog mailLog, String subject);
    }
}
