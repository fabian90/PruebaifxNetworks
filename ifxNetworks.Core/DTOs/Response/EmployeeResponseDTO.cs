using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.DTOs.Response
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; } // Additional field
        public string? Position { get; set; }   // Additional field
        public bool IsActive { get; set; }
        public int EntityId { get; set; }      // Foreign key to the Entity
        // New property to hold the Entity name
        public string? EntityName { get; set; } // Name of the associated Entity
    }
}
