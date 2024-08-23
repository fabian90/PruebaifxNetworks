using Commons.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace ifxNetworks.Core.Entities
{
    public partial class User : BaseEntity
    {
        public User() 
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string UserName { get; set; }        
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
