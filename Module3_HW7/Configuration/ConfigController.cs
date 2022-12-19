using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Module3_HW7
{
    internal static class ConfigController
    {
        private static string _configFilePath =
            "..\\..\\..\\Config\\config.json";

        internal static ConfigParameters? CurrentConfigs { get; set; }

        internal static bool ValidateConfig()
        {
            FileInfo fileInf = new FileInfo(_configFilePath);

            if (fileInf.Extension != ".json")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Application accepts only '.json' config files. " +
                    "Please create corresponding file.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else if (!fileInf.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Config file ('.json') does not exist! " +
                    "Logger is not able to save logs to file.",
                    "Please create config file and try again.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else if (fileInf.Exists)
            {
                using (FileStream fs = new FileStream(
                    _configFilePath,
                    FileMode.Open))
                {
                    try
                    {
                        ConfigParameters configParameters =
                            JsonSerializer.Deserialize<ConfigParameters>(fs);
                        CurrentConfigs = configParameters;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Config is invalid.",
                            "Please edit config file and try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
