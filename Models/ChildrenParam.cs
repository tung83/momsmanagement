using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace momsManagement.Models
{
    public class ChildrenParam
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Address { get; set; } = string.Empty;
        public string Parent1 { get; set; } = string.Empty;
        public string Parent2 { get; set; } = string.Empty;
    }
}
