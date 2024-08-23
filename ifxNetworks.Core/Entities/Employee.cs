using Commons.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.Entities
{
    public class Employee:BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string?  Phone { get; set; }
        public int? EntityId { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public Entity? Entity { get; set; }
    }
}
