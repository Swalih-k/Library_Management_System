using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Reg obj)
        {
            if (ModelState.IsValid)
            {


                _context.Register.Add(obj);
                _context.SaveChanges();
                return RedirectToAction(nameof(Register));
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(UserLogin r)
        {
            if (ModelState.IsValid)
            {

                var filtered = from l in _context.Register
                               where l.Email == r.Email && l.Password == r.Password
                               select l;
                foreach (var p in filtered)
                {

                    return new RedirectResult(url: "/User/Index", permanent: true,
                        preserveMethod: true);
                }

            }
            return View();

        }
        public async Task<IActionResult> IndexView()
        {

            return View( _context.books.ToList());
        }
       
        public async Task<IActionResult> Index()
        {

            var UserList = _context.books.ToList();


            return View(UserList);
        }
        public async Task<IActionResult> Request(int id)
        {
            _context.books.Where(x => x.Bid == id)
            .ToList()
                .ForEach(x => x.Status = "Request");
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult logout()
        {
            return new RedirectResult(url: "/Login/Login", permanent: true,
                preserveMethod: true);
        }
    }
}