using DigitalAppStructure2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAppStructure2.Controllers
{
    public class HomeController : Controller
    {

        //function
        public List<Student> GetUserList()
        {
            List<Student> s = new()
            {
                new Student{ Id=1,Name="Ram bahadur", Address="ktm" },
                new Student{ Id=2,Name="Ram bahadur", Address="ktm" },
                new Student{ Id=3,Name="Ram bahadur", Address="ktm" },
            };
            return s;
        }
        public IActionResult Index()
        {
            
            var std = GetUserList();           
            return View(std);
        }

        public IActionResult Details(int id,string name)
        {
            return Json(name);
            Student std = GetUserList().Where(x => x.Id == id).First();
            return Json(std);
            return View(std);
        }
    }
}
