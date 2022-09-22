using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class StagingTable : BaseEntity<int>
    {
        public string Company { get; set; }

        public string Year { get; set; }

        public string Value { get; set; }
    }
}
