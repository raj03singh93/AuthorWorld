using AuthorWorld.Domain.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Domain.Data
{
    public interface IDataContext
    {
        IRepository<Author> Authors { get; }
        IRepository<Books> Books { get; }
        bool SaveChanges();
    }
}
