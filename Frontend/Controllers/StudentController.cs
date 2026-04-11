using Microsoft.AspNetCore.Mvc;
using Frontend.Services;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        private IActionResult? CheckSession()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!_studentService.IsSessionValid(role)) return Redirect("/Login/Index");
            return null;
        }

        public IActionResult Index()
        {
            var check = CheckSession(); if (check != null) return check;
            var role = HttpContext.Session.GetString("UserRole");
            if (_studentService.IsStudent(role)) return RedirectToAction("Profile");
            var vm = new StudentViewModel { CurrentPage = "Index" };
            return View(vm);
        }

        public IActionResult Profile()
        {
            var check = CheckSession(); if (check != null) return check;
            var vm = new StudentViewModel { CurrentPage = "Profile" };
            return View(vm);
        }

        public IActionResult Courses()
        {
            var check = CheckSession(); if (check != null) return check;
            var vm = new StudentViewModel { CurrentPage = "Courses" };
            return View(vm);
        }
    }
}