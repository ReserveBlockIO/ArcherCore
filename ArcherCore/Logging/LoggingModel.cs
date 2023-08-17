using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Logging
{
    /// <summary>
    /// Model for creating a log variable.
    /// </summary>
    public class LoggingModel
    {
        /// <value>Property <c>Id</c> represents key for record</value>
        [BsonId]
        public long Id { get; set; }

        /// <value>Property <c>Message</c> represents the content for the log.</value>
        public string Message { get; set; }

        /// <value>Property <c>LogType</c> represents the type of logging (Info, Error, Warning, etc.)</value>
        public string LogType { get; set; }

        /// <value>Property <c>Location</c> represents the location of where the error occurred.</value>
        public string Location { get; set; }

        /// <value>Property <c>Time</c> represents the time in which the error occurred.</value>
        public DateTime Time { get; set; }
    }
}
