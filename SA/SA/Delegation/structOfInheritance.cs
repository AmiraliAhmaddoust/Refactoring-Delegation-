using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Delegation
{
   public class structOfInheritance
    {
        public string ClassParent { get; set; }
        public List<string> InterfaceParennts { get; set; }
        public string Chilld { get; set; }
        public bool IsParentAbstarct = false;
        public bool REfactorable = true;

        /// <summary>
        /// 
        /// </summary>
        public bool OpenRecurtionExistInSuperClass = false;
        public bool IsParentThorwable = false;
        public bool SynchronizingExist = false;
    }
}
