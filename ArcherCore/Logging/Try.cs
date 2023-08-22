using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Logging
{
    public static class Try
    {
        /// <summary>
        /// Auto Logging tool
        /// <example>
        /// For example:
        ///await Try.Action(() =>
        ///{
        ///    Console.WriteLine("Hey");
        ///});
        /// </code>
        /// </example>
        /// </summary>
        public static async Task Action(Action a, bool? throwException = false)
        {
            try
            {
                a();
            }
            catch (Exception ex)
            {
                //do whatever you want here
                _ = LoggingUtilities.Log($"{ex.Message.ToString()}", "Error", $"{a.Method.Name}", DateTime.UtcNow);
                #pragma warning disable CS8629 // Nullable value type may be null.
                if (throwException.Value)
                    throw new Exception(ex.Message, ex.InnerException);
                #pragma warning restore CS8629 // Nullable value type may be null.
            }
        }
    }
}
