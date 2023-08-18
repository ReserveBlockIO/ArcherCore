using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Memory
{
    public static class MemoryService
    {
        static SemaphoreSlim MemoryServiceLock = new SemaphoreSlim(1, 1);

        public static async Task MemoryLoop()
        {
            while (true)
            {
                var delay = Task.Delay(new TimeSpan(0, 0, 5));

                await MemoryServiceLock.WaitAsync();
                try
                {
                    await GetMemoryInfo();
                }
                finally
                {
                    MemoryServiceLock.Release();
                }

                await delay;
            }
        }

        public static async Task InitializeMemory(decimal overloadAmount)
        {
            Process proc = Process.GetCurrentProcess();
            var workingSetMem = proc.WorkingSet64;
            MemoryVariables.StartMemory = Math.Round((decimal)workingSetMem / 1024 / 1024, 2);
            MemoryVariables.CurrentMemory = Math.Round((decimal)workingSetMem / 1024 / 1024, 2);
            MemoryVariables.MemoryOverloadAmount = overloadAmount;
        }

        private static async Task GetMemoryInfo()
        {
            try
            {
                Process proc = Process.GetCurrentProcess();
                var workingSetMem = proc.WorkingSet64;

                MemoryVariables.CurrentMemory = Math.Round((decimal)workingSetMem / 1024 / 1024, 2);

                if (MemoryVariables.CurrentMemory >= MemoryVariables.MemoryOverloadAmount) { MemoryVariables.MemoryOverload = true; }
                else { MemoryVariables.MemoryOverload = false; }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
