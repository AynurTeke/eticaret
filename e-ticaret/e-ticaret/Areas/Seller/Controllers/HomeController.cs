using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_ticaret.Models;
using System.Security.Cryptography;
using System.Text;

namespace e_ticaret.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class HomeController : Controller
    {
        private readonly eTicaretContext _context;

        public HomeController(eTicaretContext context)
        {
            _context = context;
        }

        // GET: Seller/Home
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login([Bind("SellerEMail,SellerPassword")] Models.Seller seller)
        {
            var dbUser = _context.Sellers.FirstOrDefault(m => m.SellerEMail == seller.SellerEMail);
            SHA256 sHA256;
            byte[] passwordBytes, hashedBytes;
            string control;

            if (dbUser != null)
            {
                sHA256 = SHA256.Create();
                passwordBytes = Encoding.ASCII.GetBytes(seller.SellerEMail.Trim() + seller.SellerPassword.Trim());
                hashedBytes = sHA256.ComputeHash(passwordBytes);
                control = BitConverter.ToString(hashedBytes).Replace("-", "");

                if (control == dbUser.SellerPassword)
                {
                    this.HttpContext.Session.SetInt32("merchant", dbUser.SellerId);
                    return RedirectToAction("Index", "Products");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
