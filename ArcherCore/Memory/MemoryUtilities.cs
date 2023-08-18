using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Memory
{
    public static class MemoryUtilities
    {
        public static async Task<(decimal, decimal, bool)> GetMemoryInfo()
        {
            return (MemoryVariables.StartMemory, MemoryVariables.CurrentMemory, MemoryVariables.MemoryOverload);
        }

        public static async Task<string> GetMemoryInfoJson()
        {
            return JsonConvert.SerializeObject(new { MemoryVariables.StartMemory, MemoryVariables.CurrentMemory, MemoryVariables.MemoryOverload });
        }
    }
}
