using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIS333K_FinalProject.DAL;
using MIS333K_FinalProject.Models;

namespace MIS333K_FinalProject.Controllers
{
    public class UnavailabilityController : Controller
    {
        private readonly AppDbContext _context;

        public UnavailabilityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Unavailability
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Unavailabilities.Include(u => u.Property);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Unavailability/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unavailability = await _context.Unavailabilities
                .Include(u => u.Property)
                .FirstOrDefaultAsync(m => m.UnavailabilityId == id);
            if (unavailability == null)
            {
                return NotFound();
            }

            return View(unavailability);
        }

        // GET: Unavailability/Create
        public IActionResult Create()
        {
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "City");
            return View();
        }

        // POST: Unavailability/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnavailabilityId,Date,PropertyId")] Unavailability unavailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unavailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "City", unavailability.PropertyId);
            return View(unavailability);
        }

        // GET: Unavailability/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unavailability = await _context.Unavailabilities.FindAsync(id);
            if (unavailability == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "City", unavailability.PropertyId);
            return View(unavailability);
        }

        // POST: Unavailability/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnavailabilityId,Date,PropertyId")] Unavailability unavailability)
        {
            if (id != unavailability.UnavailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unavailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnavailabilityExists(unavailability.UnavailabilityId))
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
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "City", unavailability.PropertyId);
            return View(unavailability);
        }

        // GET: Unavailability/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unavailability = await _context.Unavailabilities
                .Include(u => u.Property)
                .FirstOrDefaultAsync(m => m.UnavailabilityId == id);
            if (unavailability == null)
            {
                return NotFound();
            }

            return View(unavailability);
        }

        // POST: Unavailability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unavailability = await _context.Unavailabilities.FindAsync(id);
            if (unavailability != null)
            {
                _context.Unavailabilities.Remove(unavailability);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnavailabilityExists(int id)
        {
            return _context.Unavailabilities.Any(e => e.UnavailabilityId == id);
        }
    }
}
