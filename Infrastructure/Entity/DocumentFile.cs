using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entity
{
    public class DocumentFile : Entity
    {
        [Required]
        
        public byte[] File { get; set; }

        public virtual List<TypeDocument> TypeDocuments { get; set; }
    }
}
