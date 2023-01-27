using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Delegation
{
     public class DatilsForEachClassMonitor
    {
        public string ClassName { get; set; }
        public int NotRefactorByAbstarct { get; set; }
        public int NotRefactorByThrowable { get; set; }
        public int NotRefactorByOpenRecursion { get; set; }
        public int NotRefactorBySyncronozation { get; set; }
        public bool Isrefactore = false;
    }
}
