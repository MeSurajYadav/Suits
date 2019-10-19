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
    public class BusinessDayController : Controller
    {
        private readonly WLMDbContext _context;

        public BusinessDayController(WLMDbContext context)
        {
            _context = context;
        }

        // GET: BusinessDay
        public async Task<IActionResult> Index()
        {
            var wLMDbContext = _context.BusinessDays.Include(b => b.Team);
            return View(await wLMDbContext.ToListAsync());
        }

        // GET: BusinessDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDay = await _context.BusinessDays
                .Include(b => b.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessDay == null)
            {
                return NotFound();
            }

            return View(businessDay);
        }

        // GET: BusinessDay/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: BusinessDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,MonthId,YearId,TeamId")] BusinessDay businessDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", businessDay.TeamId);
            return View(businessDay);
        }

        // GET: BusinessDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDay = await _context.BusinessDays.FindAsync(id);
            if (businessDay == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", businessDay.TeamId);
            return View(businessDay);
        }

        // POST: BusinessDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,MonthId,YearId,TeamId")] BusinessDay businessDay)
        {
            if (id != businessDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessDayExists(businessDay.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", businessDay.TeamId);
            return View(businessDay);
        }

        // GET: BusinessDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDay = await _context.BusinessDays
                .Include(b => b.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessDay == null)
            {
                return NotFound();
            }

            return View(businessDay);
        }

        // POST: BusinessDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessDay = await _context.BusinessDays.FindAsync(id);
            _context.BusinessDays.Remove(businessDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessDayExists(int id)
        {
            return _context.BusinessDays.Any(e => e.Id == id);
        }
    }
}
