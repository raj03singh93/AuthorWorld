using AuthorWorld.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorWorld.UI.Models
{
    public class AddEditBookViewModel
    {
        public IEnumerable<BookDTO> Books { get; set; }
        public int AuthorId { get; set; }
    }
}
