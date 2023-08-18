using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Memory
{
    public static class MemoryVariables
    {
        public static decimal StartMemory = 0M;
        public static decimal CurrentMemory = 0M;
        public static decimal MemoryOverloadAmount = 0M;
        public static bool MemoryOverload = false;
    }
}
