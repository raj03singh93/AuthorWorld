using AuthorWorld.Domain.Data;
using AuthorWorld.Domain.Data.Model;
using AuthorWorld.DTOs.DTOs;
using AuthorWorld.Service.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Service.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly IDataContext dataContext;
        private readonly IMapper mapper;

        public AuthorService(IDataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            var author = dataContext.Authors.GetSingleOrDefault(a => a.Id == id);
            author.IsVirtualDeleted = true;
            dataContext.SaveChanges();
        }

        public IEnumerable<AuthorDTO> GetAllAuthor()
        {
            var authors = dataContext.Authors.GetWhere(a => a.IsVirtualDeleted == false).ToList();
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        public AuthorDTO GetAuthorByID(int Id)
        {
            var author = dataContext.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == Id);
            //author.Books = dataContext.Books.GetWhere(b => b.AuthorId == author.Id).ToList();
            return mapper.Map<AuthorDTO>(author);
        }

        public int Save(AuthorDTO author)
        {
            var addAuthor = mapper.Map<Author>(author);
            if (author.Id == 0)
            {
                dataContext.Authors.Add(addAuthor);
            }
            else
            {
                addAuthor = dataContext.Authors.GetSingleOrDefault(a => a.Id == author.Id);
                addAuthor.Name = author.Name;
                addAuthor.PhoneNumber = author.PhoneNumber;
                addAuthor.Email = author.Email;
            }
            dataContext.SaveChanges();
            return addAuthor.Id;
        }
    }
}
