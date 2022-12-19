using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_HW7
{
    public class Starter
    {
        public static void Run()
        {
            ConfigController.ValidateConfig();
            Logger logger = Logger.GetInstance();

            List<Task> tasksToSync = new List<Task>();
            tasksToSync.Add(logger.LogAlphaAsync());
            tasksToSync.Add(logger.LogBetaAsync());
            Task.WaitAll(tasksToSync.ToArray());

            Console.WriteLine("Logs:");
            foreach (var log in logger.ReturnMemoryLogs())
            {
                if (log.Status == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(log.ErrorMessage + " " + log.LogType + " " +
                    log.Status + " " +
                    log.LogTime.ToString("hh.mm.ss.ffffff"));
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
