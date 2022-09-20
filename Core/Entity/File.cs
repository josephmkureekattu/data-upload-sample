using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class File : BaseEntity<int>
    {
        public Guid FileIdentifier { get; set; }

        public int BatchId { get; set; }

        public string FileName { get; set; }    

        public virtual Batch Batch { get; set; }

        public virtual ICollection<UploadLog> uploadLogs { get; set; }
    }
}
