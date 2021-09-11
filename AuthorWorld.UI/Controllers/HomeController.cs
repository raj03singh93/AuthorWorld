using AuthorWorld.DTOs.DTOs;
using AuthorWorld.Service.Contract;
using AuthorWorld.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorWorld.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAuthorService authorService;

        public HomeController(ILogger<HomeController> logger, IAuthorService authorService)
        {
            this.logger = logger;
            this.authorService = authorService;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Getting Author Data");
            var authors = authorService.GetAllAuthor();
            return View(authors);
        }
        public IActionResult AddEditAuthor(int Id)
        {
            logger.LogInformation("Add/edit Author Data");
            AuthorDTO author = new AuthorDTO();
            if (Id != 0)
            {
                author = authorService.GetAuthorByID(Id);
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(AuthorDTO author)
        {
            logger.LogInformation("Save Author Data");
            if (author != null)
            {
                int id = authorService.Save(author);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            logger.LogInformation("Delete Author Data");
            if (Id != 0)
            {
                authorService.Delete(Id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
