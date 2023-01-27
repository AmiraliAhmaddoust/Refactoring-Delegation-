using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Delegation
{
   public class DetailOfClass
    {
        public string ClassName { get; set; }
        public List<DtailsMehods> dtails { get; set; }
        public List<DtailsMehods> RemovedInConsdtractourbyDelegation { get; set; }
        public List<DtailsMehods> ids { get; set; }
        public List<DtailsMehods> beforeClassName { get; set; }
        public int StartClassToken { get; set; }

    }
}
