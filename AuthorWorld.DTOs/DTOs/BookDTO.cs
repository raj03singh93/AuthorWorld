using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.DTOs.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(length: 500)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int AuthorId { get; set; }
        
    }
}
