using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ASPConfig.Managers
{
    public class LogManager : ILogManager
    {
        private static Stack<string> logLevels = new Stack<string>();

        public LogManager()
        {

            // initalize stack with the levels

            logLevels.Push("error");
            logLevels.Push("warning");
            logLevels.Push("debug");

            // get the allowed level from appsettings
            var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");

            var config = configuration.Build();
            var allowedLevel = config.GetValue<string>("LogLevelAllowed", "debug");

            // remove from stack until desired level is reached

            while(allowedLevel != logLevels.Peek())
            {
                logLevels.Pop();
            }

        }

        public bool checkIfCanLog(string logLevel)
        {
            logLevel = logLevel.ToLower(); // cant always be sure

            if (logLevels.Contains(logLevel))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
