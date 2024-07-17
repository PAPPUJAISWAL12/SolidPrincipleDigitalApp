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
        private readonly CrudDigitalAppContext _appContext;
        private readonly IDataProtector _protector;
        private readonly IWebHostEnvironment _env;
        public HomeController(IStudentService service,
            CrudDigitalAppContext context,
            DataSecurityProvider datakey, IDataProtectionProvider provider, IWebHostEnvironment env)
        {
            _service = service;
            _protector = provider.CreateProtector(datakey.dataKey);
            _appContext = context;
            _env =env;
        }
        public IActionResult Index()
        {
            var s = _service.GetStudents();
            var u = s.Select(e => new UserListEdit
            {
                UserId = e.UserId,
                UserName = e.UserName,
                EmailAddress = e.EmailAddress,
                UserAddress = e.UserAddress,
                UserPassword = e.UserPassword,
                UserProfile = e.UserProfile,
                EncId = _protector.Protect(e.UserId.ToString()),
                UserRole=e.UserRole
            }).ToList();
            return View(u);
        }

        public IActionResult Details(string id)
        {
            int userid = Convert.ToInt32(_protector.Unprotect(id));
            UserList std = _service.GetStdById(userid);           
            return View(std);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserListEdit edit) {
            int maxid;
            if (_service.GetStudents().Any())
                maxid = _service.GetStudents().Max(x => x.UserId) + 1;
            else
                maxid = 1;
            edit.UserId = maxid;
            if (edit.UserFile != null)
            {
                string filename = maxid.ToString() + Guid.NewGuid() + Path.GetExtension(edit.UserFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath,"UserProfile",filename);
                using(FileStream str=new FileStream(filePath, FileMode.Create))
                {
                    edit.UserFile.CopyTo(str);
                }
                edit.UserProfile = filename;
            }


            return Json("succes");
        }
    }
}
