using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entity
{
    public class TypeDocument : Entity
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int KodUser { get; set; }
        [Required]
        public int KodDocumentFile { get; set; }
    }
}
