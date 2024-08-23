using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.DTOs.Response
{
    public class EntityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } // Para la respuesta
        public ICollection<EmployeeResponseDTO>? Employees { get; set; } // Lista de empleados
    }
}
