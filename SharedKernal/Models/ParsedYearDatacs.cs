using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Models
{
    public class ParsedYearDatacs
    {

        public string Company { get; set; }

        public List<KeyValuePair<string, string>> YearData { get; set; }
    }
}
