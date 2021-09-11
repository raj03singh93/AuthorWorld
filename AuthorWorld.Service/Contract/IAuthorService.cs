using AuthorWorld.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Service.Contract
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDTO> GetAllAuthor();
        AuthorDTO GetAuthorByID(int Id);
        int Save(AuthorDTO author);
        void Delete(int id);
    }
}
