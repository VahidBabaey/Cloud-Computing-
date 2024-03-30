using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class BaseEntity:BaseLog
    {
        public int ID { get; set; }
    }

    public class BaseLog
    {
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }

    }
}
