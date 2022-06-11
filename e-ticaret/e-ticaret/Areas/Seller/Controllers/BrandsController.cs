using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_ticaret.Models;

namespace e_ticaret.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class BrandsController : Controller
    {
        private readonly eTicaretContext _context;

        public BrandsController(eTicaretContext context)
        {
            _context = context;
        }

        // GET: Seller/Brands
        public async Task<IActionResult> Index()
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.Brands != null ? 
                          View(await _context.Brands.ToListAsync()) :
                          Problem("Entity set 'eTicaretContext.Brands'  is null.");
        }

        // GET: Seller/Brands/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Seller/Brands/Create
        public IActionResult Create()
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Seller/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] Brand brand)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Seller/Brands/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Seller/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("BrandId,BrandName")] Brand brand)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Seller/Brands/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Seller/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            int? merchantId = HttpContext.Session.GetInt32("merchant");
            if (merchantId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Brands == null)
            {
                return Problem("Entity set 'eTicaretContext.Brands'  is null.");
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(short id)
        {
          return (_context.Brands?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
