using AuthorWorld.DTOs.DTOs;
using AuthorWorld.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthorWorld.UI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ILogger<BookController> logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public ActionResult GetBooks()
        {
            logger.LogInformation("Getting Book Data");
            return PartialView();
        }

        public ActionResult AddEditBook(int Id, [FromQuery]int AuthorId)
        {
            logger.LogInformation("Add/edit Book Data");
            BookDTO book = new BookDTO();
            book.AuthorId = AuthorId;
            if (Id != 0)
            {
                book = bookService.GetBookByID(Id);
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookDTO book)
        {
            logger.LogInformation("Saving Book Data");
            if (book != null)
            {
                int id = bookService.Save(book);
            }
            return RedirectToAction("AddEditAuthor", "Home", new { Id = book.AuthorId });
        }

        public IActionResult Delete(int Id)
        {
            logger.LogInformation("Delete Book Data");
            int authorId = 1;
            if (Id != 0)
            {
                authorId = bookService.Delete(Id);
            }
            return RedirectToAction("AddEditAuthor", "Home", new { Id = authorId });
        }
    }
}
