using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.DTOs.Request
{
    public class EntityRequestDTOCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } // Nombre de la entidad
        public bool IsActive { get; set; } // Estado de la entidad, por defecto true
    }
}
