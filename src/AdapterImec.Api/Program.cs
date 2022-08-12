using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdapterImec.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UpdateNLogConfig();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
            .UseNLog();

        private static void UpdateNLogConfig()
        {
            // find NLog.config
            var currentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (currentFolder is null)
            {
                return;
            }

            var nlogFolder = Path.Combine(currentFolder, "nlog.config");

            if (!File.Exists(nlogFolder))
            {
                Console.WriteLine($"File does not exists: {nlogFolder}");
                return;
            }

            var configText = File.ReadAllText(nlogFolder);

            if (configText is null)
            {
                Console.WriteLine($"Empty file: {nlogFolder}");
                return;
            }

            configText = SetValueForVariableName(configText, "environment", "ASPNETCORE_ENVIRONMENT");
            configText = SetValueForVariableName(configText, "application-name", "NEW_RELIC_APP_NAME");
            configText = SetValueForVariableName(configText, "logstash-credentials", "LOGSTASH_LOGGINGCREDENTIALS");
            configText = SetValueForVariableName(configText, "logstash-url", "LOGSTASH_LOGGINGURL");

            File.WriteAllText(nlogFolder, configText);
        }

        private static string SetValueForVariableName(string configFile, string variableName, string environmentName)
        {
            var value = Environment.GetEnvironmentVariable(environmentName);

            if (String.IsNullOrEmpty(value))
            {
                return configFile;
            }

            // look for variabele with variableName
            // <variable name="environment" value="DEV" />
            var pattern = $"<variable name=\"{variableName}\" value=\"(.*?)\" \\/>";
            var originalMatch = System.Text.RegularExpressions.Regex.Matches(configFile, pattern).FirstOrDefault();

            if (originalMatch != null && originalMatch.Success)
            {
                var originalLine = originalMatch.Groups[0].Value;
                var originalValue = $"\"{originalMatch.Groups[1].Value}\"";

                var newValue = $"\"{value}\"";
                var newLine = originalLine.Replace(originalValue, newValue);

                var newConfigFile = configFile.Replace(originalLine, newLine);

                if (newConfigFile != configFile)
                {
                    Console.WriteLine($"Nlog.Config -> Replace '{variableName}' from [{originalValue}] to [{newValue}]");
                }

                configFile = newConfigFile;
            }
            else
            {
                Console.WriteLine($"Nlog.Config -> No line for '{variableName}' found !! ('{environmentName}' = '{value}')");
            }

            return configFile;
        }
    }
}
