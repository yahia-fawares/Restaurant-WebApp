using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Models;

namespace MyRestaurant.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LoginAndRegisterController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,ImgFile")] UserCustomer userCustomer, string username, string password)
        {
            //To upload Img 

            if (ModelState.IsValid)
            {
                if (userCustomer.ImgFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userCustomer.ImgFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Imges/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userCustomer.ImgFile.CopyToAsync(fileStream);
                    }
                    userCustomer.ImagePath = fileName;
                }
                _context.Add(userCustomer);
                await _context.SaveChangesAsync();

                UserLogin login = new UserLogin();
                login.UserName = username;
                login.Password = password;
                login.CustomerId = userCustomer.Id;
                login.RoleId = 2;
                _context.Add(login);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(userCustomer);
        }



        


       
      

        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("UserName,Password")] UserLogin userLogin)
        {
            var auth = _context.UserLogins.Where(x => x.UserName == userLogin.UserName && x.Password == userLogin.Password).SingleOrDefault();
            if (auth != null)
            {
                switch (auth.RoleId)
                {
                    

                    case 1://Admin
                        HttpContext.Session.SetString("AdminName", auth.UserName);
                        HttpContext.Session.SetInt32("AdminId", (int)auth.Id);

                        return RedirectToAction("Index", "Admin");

                    case 2://Customer 
                        HttpContext.Session.SetInt32("CustomerId", (int)auth.CustomerId);
                        return RedirectToAction("Index", "Home");

                }

            }

            return View();
        }
    }
}
