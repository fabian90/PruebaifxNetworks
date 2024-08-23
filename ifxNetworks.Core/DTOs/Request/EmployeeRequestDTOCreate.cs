using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.DTOs.Request
{
    public class EmployeeRequestDTOCreate
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; } // Additional field
        public string? Position { get; set; }   // Additional field
        public int EntityId { get; set; }
        public bool IsActive { get; set; } = true;// Foreign key to the Entity
    }
}
