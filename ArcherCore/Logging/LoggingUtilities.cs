using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Logging
{
    public static class LoggingUtilities
    {
        /// <summary>
        /// Add a row to the log queue
        /// </summary>
        /// <param name="message">input string</param>
        /// <param name="logType">input string</param>
        /// <param name="location">input string</param>
        /// <param name="time">input DateTime</param>
        public static async Task Log(string message, string logType, string location, DateTime time)
        {
            LoggingVariables.LogQueue.Enqueue((message, logType, location, DateTime.Now));
        }

        public static async Task<string[]?> GetLogFromText()
        {
            try
            {
                var path = Path.Combine(LoggingVariables.LoggingPath, "Log.txt");
                var fileExist = File.Exists(path);
                if (fileExist)
                {
                    var readLogLines = await File.ReadAllLinesAsync(path);
                    return readLogLines;
                }

            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            
            return null;
        }

        /// <summary>
        /// Gets the most recent 100 records by default for a log
        /// </summary>
        /// <param name="logCount">input int</param>
        /// <returns>
        /// Returns a List<LoggingModel>?
        /// </returns>
        public static async Task<List<LoggingModel>?> GetLog(int logCount = 100)
        {
            var logsDb = LoggingVariables.LogDb.GetCollection<LoggingModel>(LoggingVariables.ARCHER_LOG);
            var recentLogs = logsDb.Find(Query.All(Query.Descending)).Take(logCount).ToList();

            if(recentLogs.Count > 0 )
            {
                return recentLogs;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the most recent 100 records by default for a log and serializes it.
        /// </summary>
        /// <param name="logCount">input int</param>
        /// <returns>
        /// Returns a JSON string?
        /// </returns>
        public static async Task<string?> GetLogJson(int logCount = 100)
        {
            var logsDb = LoggingVariables.LogDb.GetCollection<LoggingModel>(LoggingVariables.ARCHER_LOG);
            var recentLogs = logsDb.Find(Query.All(Query.Descending)).Take(logCount).ToList();

            if (recentLogs.Count > 0)
            {
                return JsonConvert.SerializeObject(recentLogs);
            }
            else
            {
                return null;
            }
        }
    }
}
