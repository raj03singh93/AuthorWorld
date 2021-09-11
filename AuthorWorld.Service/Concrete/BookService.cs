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
    public class BookService : IBookService
    {
        private readonly IMapper mapper;
        private readonly IDataContext dataContext;

        public BookService(IMapper mapper, IDataContext dataContext)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
        }

        public int Delete(int id)
        {
            int authorId;
            var book = dataContext.Books.GetSingleOrDefault(a => a.Id == id);
            book.isVirtualDeleted = true;
            authorId = book.AuthorId;
            dataContext.SaveChanges();
            return authorId;
        }

        public BookDTO GetBookByID(int Id)
        {
            var author = dataContext.Books.GetSingleOrDefault(a => a.Id == Id);
            return mapper.Map<BookDTO>(author);
        }

        public int Save(BookDTO book)
        {
            var addBook = mapper.Map<Books>(book);
            if (book.Id == 0)
            {
                dataContext.Books.Add(addBook);
            }
            else
            {
                addBook = dataContext.Books.GetSingleOrDefault(a => a.Id == book.Id);
                addBook.Name = book.Name;
                addBook.Price = book.Price;
                addBook.Quantity = book.Quantity;
                addBook.DatePublished = DateTime.Now;
            }
            dataContext.SaveChanges();
            return addBook.Id;
        }
    }
}
