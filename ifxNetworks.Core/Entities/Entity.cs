using Commons.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.Entities
{
    public class Entity:BaseEntity
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public ICollection<Employee>? Employees { get; set; }
    }
}
