using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_HW7
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private static Logger instance;
        private List<LogEntry> _logEntries;
        private string _logBackupEntries;
        private int _totalLogs = 0;
        private Logger()
        {
            _logEntries = new List<LogEntry>();
            OnBackupCreationTime += BackupHandler.CreateBackup;
        }

        public delegate Task BackupDelegate(string logs);
        public event BackupDelegate OnBackupCreationTime;
        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }

            return instance;
        }

        public List<LogEntry> ReturnMemoryLogs()
        {
            return _logEntries;
        }

        public string ReturnBackupLogs()
        {
            return _logBackupEntries;
        }

        public async void Log(bool logType)
        {
            for (int i = 0; i < 50; i++)
            {
                if (logType == true)
                {
                    LogEntry logEntry = new LogEntry()
                    {
                        Status = true,
                        LogType = LogEntry.LogTypeEnum.Warning,
                        ErrorMessage = i + "Alpha log",
                        LogTime = DateTime.Now
                    };
                    lock (_lock)
                    {
                    _logEntries.Add(logEntry);
                    _logBackupEntries +=
                        "[" + logEntry.LogTime.ToString("hh:mm:ss:ffffff") +
                        "] [" + logEntry.LogType + "] ["
                        + logEntry.ErrorMessage + "]\n";
                    _totalLogs++;
                    }
                }
                else
                {
                    LogEntry logEntry = new LogEntry()
                    {
                        Status = false,
                        LogType = LogEntry.LogTypeEnum.Error,
                        ErrorMessage = i + "Beta log",
                        LogTime = DateTime.Now
                    };
                    lock (_lock)
                    {
                        _logEntries.Add(logEntry);
                        _logBackupEntries += "[" +
                            logEntry.LogTime.ToString("hh:mm:ss:ffffff") +
                            "] [" + logEntry.LogType + "] ["
                            + logEntry.ErrorMessage + "]\n";
                        _totalLogs++;
                    }
                }

                if (_totalLogs % ConfigController.CurrentConfigs.Number == 0)
                {
                        OnBackupCreationTime(_logBackupEntries);
                }
            }
        }

        public async Task LogAlphaAsync()
        {
            await Task.Run(() => Log(true));
        }

        public async Task LogBetaAsync()
        {
            await Task.Run(() => Log(false));
        }
    }
}
