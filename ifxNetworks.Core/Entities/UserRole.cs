
using Commons.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace ifxNetworks.Core.Entities
{
    public class UserRole : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }

        public virtual Role Roles { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
