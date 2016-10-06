using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTFG.Model
{
    public partial class MacsList
    {
        public MacsList()
        {
            macList = new List<string>();
        }

        public virtual ICollection<string> macList { get; set; }
    }
}
