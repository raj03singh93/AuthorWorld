using AuthorWorld.Domain.Data;
using AuthorWorld.Domain.Data.Model;
using AuthorWorld.DTOs.DTOs;
using AuthorWorld.Service.Contract;
using AuthorWorld.Service.Extention;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorWorld.Service.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly IDataContext dataContext;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public AuthorService(IDataContext dataContext, IMapper mapper, IDistributedCache distributedCache)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public void Delete(int id)
        {
            var author = dataContext.Authors.GetSingleOrDefault(a => a.Id == id);
            author.IsVirtualDeleted = true;
            dataContext.SaveChanges();
        }

        public IEnumerable<AuthorDTO> GetAllAuthor()
        {
            var authors = distributedCache.GetCache<List<Author>>("Author_All");

            if (authors == null)
            {
                authors = dataContext.Authors.GetWhere(a => a.IsVirtualDeleted == false).ToList();
                distributedCache.AddCache<List<Author>>("Author_All", authors);
                Thread.Sleep(7000);
            }

            return mapper.Map<List<AuthorDTO>>(authors);
        }

        public AuthorDTO GetAuthorByID(int Id)
        {
            var author = distributedCache.GetCache<Author>($"Author_{Id}");
            if (author == null)
            {
                author = dataContext.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == Id);
                distributedCache.AddCache<Author>($"Author_{Id}", author);
                Thread.Sleep(7000);
            }
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

            distributedCache.AddCache<Author>($"Author_{addAuthor.Id}", addAuthor);

            var authors = distributedCache.GetCache<List<Author>>("Author_All");

            if (authors != null)
            {
                var checkAuthor = authors.Where(a => a.Id == addAuthor.Id).FirstOrDefault();
                if (checkAuthor != null) authors.Remove(authors.Where(a => a.Id == addAuthor.Id).FirstOrDefault());

                authors.Add(addAuthor);
                distributedCache.AddCache<List<Author>>("Author_All", authors);
            }

            return addAuthor.Id;
        }
    }
}
