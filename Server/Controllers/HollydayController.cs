using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Contexts;

namespace Server.Controllers
{
    public class HollydayController : Controller
    {
        private readonly WLMDbContext _context;

        public HollydayController(WLMDbContext context)
        {
            _context = context;
        }

        // GET: Hollyday
        public async Task<IActionResult> Index()
        {
            var wLMDbContext = _context.HollyDays.Include(h => h.CreatedBy);
            return View(await wLMDbContext.ToListAsync());
        }

        // GET: Hollyday/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollyday = await _context.HollyDays
                .Include(h => h.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hollyday == null)
            {
                return NotFound();
            }

            return View(hollyday);
        }

        // GET: Hollyday/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: Hollyday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DayOfWeek,Description,CreatedById")] Hollyday hollyday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hollyday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Employees, "Id", "Id", hollyday.CreatedById);
            return View(hollyday);
        }

        // GET: Hollyday/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollyday = await _context.HollyDays.FindAsync(id);
            if (hollyday == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Employees, "Id", "Id", hollyday.CreatedById);
            return View(hollyday);
        }

        // POST: Hollyday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DayOfWeek,Description,CreatedById")] Hollyday hollyday)
        {
            if (id != hollyday.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hollyday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HollydayExists(hollyday.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Employees, "Id", "Id", hollyday.CreatedById);
            return View(hollyday);
        }

        // GET: Hollyday/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollyday = await _context.HollyDays
                .Include(h => h.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hollyday == null)
            {
                return NotFound();
            }

            return View(hollyday);
        }

        // POST: Hollyday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hollyday = await _context.HollyDays.FindAsync(id);
            _context.HollyDays.Remove(hollyday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HollydayExists(int id)
        {
            return _context.HollyDays.Any(e => e.Id == id);
        }
    }
}
