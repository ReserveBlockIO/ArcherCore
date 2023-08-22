using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Files
{
    public class FileVariables
    {
        // Test only, do not execute changes
        public bool TestNoModify { get; set; }

        // Cancel waiting operations
        public CancellationToken Cancel { get; set; } = CancellationToken.None;

        // Number of retries
        public int RetryCount { get; set; } = 1;

        // Wait time between retries in seconds
        public int RetryWaitTime { get; set; } = 5;

        public bool RetryWaitForCancel()
        {
            return Cancel.WaitHandle.WaitOne(RetryWaitTime * 1000);
        }
    }
}
