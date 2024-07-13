using DigitalAppStructure2.Models;
using DigitalAppStructure2.Security;
using DigitalAppStructure2.SolidPrinciple;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAppStructure2.Controllers
{  
    public class HomeController : Controller
    {
        //declaretion
        private readonly IStudentService _service;
        private readonly IDataProtector _protector;
        public HomeController(IStudentService service,DataSecurityProvider datakey, IDataProtectionProvider provider)
        {
            _service = service;
            _protector = provider.CreateProtector(datakey.dataKey);
        }
        public IActionResult Index()
        {            
            var std = _service.GetStudents();
            var s = std.Select(e => new Student {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                encId = _protector.Protect(e.Id.ToString())
            }).ToList();
            return View(s);
        }

        public IActionResult Details(string id)
        {
            int userid = Convert.ToInt32(_protector.Unprotect(id));
            Student std = _service.GetStdById(userid);           
            return View(std);
        }
    }
}
