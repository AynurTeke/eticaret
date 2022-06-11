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
    public class ItemStatusController : Controller
    {
        private readonly eTicaretContext _context;

        public ItemStatusController(eTicaretContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemStatus
        public async Task<IActionResult> Index()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.ItemStatuses != null ? 
                          View(await _context.ItemStatuses.ToListAsync()) :
                          Problem("Entity set 'eTicaretContext.ItemStatuses'  is null.");
        }

        // GET: Admin/ItemStatus/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatuses == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatuses
                .FirstOrDefaultAsync(m => m.ItemStatusId == id);
            if (itemStatus == null)
            {
                return NotFound();
            }

            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Create
        public IActionResult Create()
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Admin/ItemStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemStatusId,ItemStatusName")] ItemStatus itemStatus)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(itemStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatuses == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatuses.FindAsync(id);
            if (itemStatus == null)
            {
                return NotFound();
            }
            return View(itemStatus);
        }

        // POST: Admin/ItemStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ItemStatusId,ItemStatusName")] ItemStatus itemStatus)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != itemStatus.ItemStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemStatusExists(itemStatus.ItemStatusId))
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
            return View(itemStatus);
        }

        // GET: Admin/ItemStatus/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.ItemStatuses == null)
            {
                return NotFound();
            }

            var itemStatus = await _context.ItemStatuses
                .FirstOrDefaultAsync(m => m.ItemStatusId == id);
            if (itemStatus == null)
            {
                return NotFound();
            }

            return View(itemStatus);
        }

        // POST: Admin/ItemStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            string? adminId = HttpContext.Session.GetString("guest");
            if (adminId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.ItemStatuses == null)
            {
                return Problem("Entity set 'eTicaretContext.ItemStatuses'  is null.");
            }
            var itemStatus = await _context.ItemStatuses.FindAsync(id);
            if (itemStatus != null)
            {
                _context.ItemStatuses.Remove(itemStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemStatusExists(short id)
        {
          return (_context.ItemStatuses?.Any(e => e.ItemStatusId == id)).GetValueOrDefault();
        }
    }
}
