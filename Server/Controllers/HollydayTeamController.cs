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
    public class HollydayTeamController : Controller
    {
        private readonly WLMDbContext _context;

        public HollydayTeamController(WLMDbContext context)
        {
            _context = context;
        }

        // GET: HollydayTeam
        public async Task<IActionResult> Index()
        {
            var wLMDbContext = _context.HollydayTeam.Include(h => h.Hollyday).Include(h => h.Team);
            return View(await wLMDbContext.ToListAsync());
        }

        // GET: HollydayTeam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollydayTeam = await _context.HollydayTeam
                .Include(h => h.Hollyday)
                .Include(h => h.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hollydayTeam == null)
            {
                return NotFound();
            }

            return View(hollydayTeam);
        }

        // GET: HollydayTeam/Create
        public IActionResult Create()
        {
            ViewData["HollydayId"] = new SelectList(_context.HollyDays, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: HollydayTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamId,HollydayId")] HollydayTeam hollydayTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hollydayTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HollydayId"] = new SelectList(_context.HollyDays, "Id", "Id", hollydayTeam.HollydayId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", hollydayTeam.TeamId);
            return View(hollydayTeam);
        }

        // GET: HollydayTeam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollydayTeam = await _context.HollydayTeam.FindAsync(id);
            if (hollydayTeam == null)
            {
                return NotFound();
            }
            ViewData["HollydayId"] = new SelectList(_context.HollyDays, "Id", "Id", hollydayTeam.HollydayId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", hollydayTeam.TeamId);
            return View(hollydayTeam);
        }

        // POST: HollydayTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamId,HollydayId")] HollydayTeam hollydayTeam)
        {
            if (id != hollydayTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hollydayTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HollydayTeamExists(hollydayTeam.Id))
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
            ViewData["HollydayId"] = new SelectList(_context.HollyDays, "Id", "Id", hollydayTeam.HollydayId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", hollydayTeam.TeamId);
            return View(hollydayTeam);
        }

        // GET: HollydayTeam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hollydayTeam = await _context.HollydayTeam
                .Include(h => h.Hollyday)
                .Include(h => h.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hollydayTeam == null)
            {
                return NotFound();
            }

            return View(hollydayTeam);
        }

        // POST: HollydayTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hollydayTeam = await _context.HollydayTeam.FindAsync(id);
            _context.HollydayTeam.Remove(hollydayTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HollydayTeamExists(int id)
        {
            return _context.HollydayTeam.Any(e => e.Id == id);
        }
    }
}
