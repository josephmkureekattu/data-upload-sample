using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Batch : BaseEntity<int>
    {
        public Guid BatchIdentifier { get; set; }

        public virtual ICollection<File> files { get; set; }
    }
}
