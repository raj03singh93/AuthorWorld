using AuthorWorld.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Service.Contract
{
    public interface IBookService
    {
        BookDTO GetBookByID(int Id);
        int Save(BookDTO book);
        int Delete(int id);
    }
}
