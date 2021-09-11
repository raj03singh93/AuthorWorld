using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorWorld.Domain.Data.Model
{
    public partial class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(length: 100)]
        public string Name { get; set; }
        [MaxLength(length: 150)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(length: 13)]
        public string PhoneNumber { get; set; }
        public bool IsVirtualDeleted { get; set; }
        public List<Books> Books { get; set; }
    }
}
