using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Entity
{
    public class RoleUser : Entity
    {
        [Required]
        public string Role { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
