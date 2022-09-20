using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class BatchDTO
    {
        public string BatchIdentifier { get; set; }

        public string[] FileNames { get; set; }
    }
}
