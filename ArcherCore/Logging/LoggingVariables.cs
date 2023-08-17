using LiteDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Logging
{
    public static class LoggingVariables
    {
        public static string LoggingPath = "";
        public static bool UseDb = false;
        public static ConcurrentQueue<(string Message, string LogType, string Location, DateTime Time)> LogQueue = new ConcurrentQueue<(string, string, string, DateTime)>();
        public static LiteDatabase LogDb { get; set; }
        public const string ARCHER_DB_NAME = @"log.db";
        public const string ARCHER_LOG = "archer_log";
    }
}
