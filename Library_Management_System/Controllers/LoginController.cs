using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Registration(Reg reg)
        {
            if (ModelState.IsValid)
            {
                _context.Register.Add(reg);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Registration));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Book obj)
        {
            if (ModelState.IsValid)
            {


                _context.books.Add(obj);
                _context.SaveChanges();
                return RedirectToAction(nameof(Registration));
            }
            return View();
        }
        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(AdminLogin Model)
        {
            if (ModelState.IsValid)
            {

                if (Model.Name == "admin" && Model.Password == "12")
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {

                    ViewBag.ErrorMessage = "Invalid username or password!";
                }
            }
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    var q = from c in _context.books where c.Status == "Pending" select c;
        //    return View(q.ToList());
        //}
        //public async Task<IActionResult> Index1()
        //{

        //    return View(await _context.books.ToListAsync());
        //}
        public async Task<IActionResult> Index1()
        {

            return View( _context.books.ToList());
        }
        public async Task<IActionResult> Index()
        {
            var q = from c in _context.books where c.Status == "Request" select c;
            return View(q.ToList());
        }
        public async Task<IActionResult> Approve(int id)
        {
            _context.books.Where(x => x.Bid == id)
            .ToList()
                .ForEach(x => x.Status = "Approve");
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null || _context.books == null)
            {
                return NotFound();
            }
            var cat = await _context.books.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book books)
        {
            if (id != books.Bid)
            {
                return NotFound();
            }

            _context.Update(books);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int Id)
        {
            if (_context.books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.book' is null");
            }

            var products = await _context.books.FindAsync(Id);
            if (products != null)
            {
                _context.books.Remove(products);
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search()
        {
            var objBookList = _context.books.ToList();
            return View(objBookList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string BookName)
        {
            if (BookName == null)
            {
                return NotFound();
            }


            var books = from r in _context.books where r.BookName == BookName select r;
            if (BookName == null || _context.books == null)
            {
                return NotFound();
            }
            return View(books.ToList());
        }
        public IActionResult SearchAuthor()
        {
            var objBookList = _context.books.ToList();
            return View(objBookList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAuthor(string Author)
        {
            if (Author == null)
            {
                return NotFound();
            }


            var books = from r in _context.books where r.Author == Author select r;
            if (Author == null || _context.books == null)
            {
                return NotFound();
            }
            return View(books.ToList());
        }
        public IActionResult logout()
        {
            return new RedirectResult(url: "/User/Login", permanent: true,
                preserveMethod: true);
        }

    }
}

