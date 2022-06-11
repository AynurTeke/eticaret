using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_ticaret.Models;

namespace e_ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentMethodsController : Controller
    {
        private readonly eTicaretContext _context;

        public PaymentMethodsController(eTicaretContext context)
        {
            _context = context;
        }

        // GET: Admin/PaymentMethods
        public async Task<IActionResult> Index()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.PaymentMethods != null ? 
                          View(await _context.PaymentMethods.ToListAsync()) :
                          Problem("Entity set 'eTicaretContext.PaymentMethods'  is null.");
        }

        // GET: Admin/PaymentMethods/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethods/Create
        public IActionResult Create()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Admin/PaymentMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentMethodId,PaymentMethodName")] PaymentMethod paymentMethod)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: Admin/PaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("PaymentMethodId,PaymentMethodName")] PaymentMethod paymentMethod)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != paymentMethod.PaymentMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.PaymentMethodId))
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
            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: Admin/PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.PaymentMethods == null)
            {
                return Problem("Entity set 'eTicaretContext.PaymentMethods'  is null.");
            }
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethods.Remove(paymentMethod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(short id)
        {
          return (_context.PaymentMethods?.Any(e => e.PaymentMethodId == id)).GetValueOrDefault();
        }
    }
}
