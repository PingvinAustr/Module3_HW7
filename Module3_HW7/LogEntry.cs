using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_HW7
{
   public class LogEntry
        {
            public LogEntry()
            {
                Status = null;
                ErrorMessage = string.Empty;
            }

            public LogEntry(bool status, string message, LogTypeEnum logType, DateTime dateTime)
            {
                Status = status;
                ErrorMessage = message;
                LogType = logType;
                LogTime = dateTime;
            }

            public enum LogTypeEnum
            {
                Info,
                Warning,
                Error
            }

            public bool? Status { get; set; }
            public string ErrorMessage { get; set; }
            public LogTypeEnum LogType { get; set; }
            public DateTime LogTime { get; set; }
        }
    }
