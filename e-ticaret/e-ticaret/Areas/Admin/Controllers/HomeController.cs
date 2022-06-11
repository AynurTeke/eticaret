using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_ticaret.Areas.Admin.Models;
using System.Security.Cryptography;
using System.Text;


namespace e_ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }

        // GET: Admin/Home
        public  IActionResult Index()
        {
              return View();

        }
        public IActionResult Login([Bind("UserEMail,UserPassword")]User user)
        {
            var dbUser = _context.Users.FirstOrDefault(m => m.UserEMail == user.UserEMail);
            SHA256 sHA256;
            byte[] passwordBytes, hashedBytes;
            string control;

            if (dbUser != null)
            {
                sHA256 = SHA256.Create();
                passwordBytes = Encoding.ASCII.GetBytes(user.UserEMail.Trim() + user.UserPassword.Trim());
                hashedBytes = sHA256.ComputeHash(passwordBytes);
                control = BitConverter.ToString(hashedBytes).Replace("-", "");

                if (control == dbUser.UserPassword)
                {
                    this.HttpContext.Session.SetString("guest", dbUser.UserId.ToString());
                    this.HttpContext.Session.SetString("viewUsers", dbUser.ViewUsers.ToString());
                    this.HttpContext.Session.SetString("createUser", dbUser.CreateUser.ToString());
                    this.HttpContext.Session.SetString("deleteUser", dbUser.DeleteUser.ToString());
                    this.HttpContext.Session.SetString("editUser", dbUser.EditUser.ToString());
                    this.HttpContext.Session.SetString("viewSellers", dbUser.ViewSellers.ToString());
                    this.HttpContext.Session.SetString("createSeller", dbUser.CreateSeller.ToString());
                    this.HttpContext.Session.SetString("deleteSeller", dbUser.DeleteSeller.ToString());
                    this.HttpContext.Session.SetString("editSeller", dbUser.EditSeller.ToString());
                    this.HttpContext.Session.SetString("viewCategories", dbUser.ViewCategories.ToString());
                    this.HttpContext.Session.SetString("createCategory", dbUser.CreateCategory.ToString());
                    this.HttpContext.Session.SetString("deleteCategory", dbUser.DeleteCategory.ToString());
                    this.HttpContext.Session.SetString("editCategory", dbUser.EditCategory.ToString());
                    this.HttpContext.Session.SetString("deleteProduct", dbUser.DeleteProduct.ToString());
                    this.HttpContext.Session.SetString("editProduct", dbUser.EditProduct.ToString());
                    //...
                    //...
                    return RedirectToAction("Index","Users");
                    //Response.Redirect("~/Admin/users/Index"); üst satırdaaki sayfaya gitmenin muadil kodu.
                }
              
            }
            return RedirectToAction("Index");
        }
    }
}
