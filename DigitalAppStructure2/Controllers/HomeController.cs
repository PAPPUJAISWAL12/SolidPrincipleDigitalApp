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
            return View();
        }

        public IActionResult GetUserList()
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
                UserRole = e.UserRole
            }).ToList();
            return PartialView("_GetUserList",u);
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
            //Mapping
            UserList u = new()
            {
                UserId = edit.UserId,
                EmailAddress = edit.EmailAddress,
                UserAddress = edit.UserAddress,
                UserName = edit.UserName,
                UserPassword = edit.UserPassword,
                UserProfile = edit.UserProfile,
                UserRole = edit.UserRole,
                UserStatus = true
            };
            if (u != null)
            {
                _service.AddStd(u);
                return Content("success");
            }
            else
            {
                return Content("failed");
            }

          
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            int uid =Convert.ToInt32(_protector.Unprotect(id));
            var u = _appContext.UserLists.Where(x=>x.UserId==uid).FirstOrDefault();
     
            UserListEdit e = new()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserPassword = u.UserPassword,
                UserRole = u.UserRole,
                UserProfile = u.UserProfile,
                EmailAddress = u.EmailAddress,
                UserAddress = u.UserAddress,
                UserStatus = u.UserStatus
            };
            ViewData["psw"] = u.UserPassword;
            return View(e);
        }

        [HttpPost]
        public IActionResult Edit(UserListEdit edit)
        {
            if (edit.UserFile != null)
            {
                string filename = "UpdatedImage" + Guid.NewGuid() + Path.GetExtension(edit.UserFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "UserProfile", filename);
                using (FileStream str = new FileStream(filePath, FileMode.Create))
                {
                    edit.UserFile.CopyTo(str);
                }
                edit.UserProfile = filename;
            }
            //Mapping
            UserList u = new()
            {
                UserId = edit.UserId,
                EmailAddress = edit.EmailAddress,
                UserAddress = edit.UserAddress,
                UserName = edit.UserName,
                UserPassword = edit.UserPassword,
                UserProfile = edit.UserProfile,
                UserRole = edit.UserRole,
                UserStatus = true
            };
          
            _service.UpdateStd(u);
            return Json(edit);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteStd(id);
                return Content("success");
            }
            catch (Exception ex)
            {
                return Json(ex,"Failed");
            }
        }
    }
}
