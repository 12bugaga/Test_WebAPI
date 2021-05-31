using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Entity
{
    public class User : Entity
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string UserName{ get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }
        public string Status { get; set; }
        [Required]
        [Range(1, 2)]
        public int KodRole { get; set; }

        public virtual List<TypeDocument> TypeDocuments { get; set; }
    }
}
