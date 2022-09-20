using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class UploadLog : BaseEntity<int>
    {
        public int FileId { get; set; }

        public string Message { get; set; }

        public File file { get; set; }  
    }
}
