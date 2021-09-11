using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorWorld.DTOs.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(length: 100)]
        public string Name { get; set; }
        [MaxLength(length: 150)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(length: 11)]
        public string PhoneNumber { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
