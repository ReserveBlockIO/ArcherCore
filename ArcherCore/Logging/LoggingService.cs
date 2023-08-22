using LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Logging
{
    public static class LoggingService
    {
        /// <summary>
        /// Starts the logging loop for logging services. No exit or stop parameters. Will stop when app stops.
        /// </summary>
        public static async Task LogLoop()
        {
            while (true)
            {
                while (LoggingVariables.LogQueue.Count > 0)
                {
                    if (LoggingVariables.LogQueue.TryDequeue(out var content))
                    {
                        if(!LoggingVariables.UseDb)
                        {
                            var text = "[" + content.Time + "]" + "[" + content.LogType + "]" + "<|!*!|>" + "[" + content.Location + "]" + "<|!*!|>" + content.Message;
                            await File.AppendAllTextAsync(LoggingVariables.LoggingPath + "Log.txt", Environment.NewLine + text);
                        }
                        else
                        {
                            var logsDb = LoggingVariables.LogDb.GetCollection<LoggingModel>(LoggingVariables.ARCHER_LOG);
                            if(logsDb != null)
                            {
                                var log = new LoggingModel { 
                                    Location = content.Location,
                                    LogType = content.LogType,
                                    Message = content.Message,
                                    Time = content.Time
                                };

                                logsDb.Insert(log);
                            }
                        }
                    }
                }

                await Task.Delay(20);
            }
        }

        /// <summary>
        /// Starts the DB Service for logging service.
        /// </summary>
        public static async Task InitializeDb()
        {
            var mapper = new BsonMapper();
            mapper.RegisterType<DateTime>(
                value => value.ToString("o", CultureInfo.InvariantCulture),
                bson => DateTime.ParseExact(bson, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
            mapper.RegisterType<DateTimeOffset>(
                value => value.ToString("o", CultureInfo.InvariantCulture),
                bson => DateTimeOffset.ParseExact(bson, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));

            string path = LoggingVariables.LoggingPath;
            LoggingVariables.LogDb = new LiteDatabase(new ConnectionString { Filename = path + LoggingVariables.ARCHER_DB_NAME, Connection = ConnectionType.Direct, ReadOnly = false }, mapper);
        }
    }
}
