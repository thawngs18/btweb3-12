using Microsoft.AspNetCore.Mvc;
using btweb3_12.Models;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(book);
                return RedirectToAction("Index");
            }
            else
            {
                // Log the errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
