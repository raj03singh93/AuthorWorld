using AuthorWorld.Domain.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Domain.Data
{
    public class DataContext : IDataContext
    {
        private readonly AppDbContext context;
        public DataContext(AppDbContext context)
        {
            this.context = context;

        }
        private IRepository<Author> authors;
        public IRepository<Author> Authors 
        {
            get
            {
                if (authors == null)
                    authors = new Repository<Author>(context);
                return authors;
            }
        }
        private IRepository<Books> books;

        public IRepository<Books> Books 
        {
            get 
            {
                if (books == null)
                    books = new Repository<Books>(context);
                return books;
            }
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
