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
    public class TaskController : Controller
    {
        private readonly WLMDbContext _context;

        public TaskController(WLMDbContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var wLMDbContext = _context.Tasks.Include(t => t.PrimaryOwner).Include(t => t.Reviewer).Include(t => t.SecondaryOwner).Include(t => t.Team);
            return View(await wLMDbContext.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.PrimaryOwner)
                .Include(t => t.Reviewer)
                .Include(t => t.SecondaryOwner)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            ViewData["PrimaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["ReviewerId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["SecondaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,CreatedOn,Priority,BusinessDT,Title,Description,IsCommissioned,PrimaryOwnerId,SecondaryOwnerId,ReviewerId,TeamId")] 
            Server.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrimaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.PrimaryOwnerId);
            ViewData["ReviewerId"] = new SelectList(_context.Employees, "Id", "Id", task.ReviewerId);
            ViewData["SecondaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.SecondaryOwnerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", task.TeamId);
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["PrimaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.PrimaryOwnerId);
            ViewData["ReviewerId"] = new SelectList(_context.Employees, "Id", "Id", task.ReviewerId);
            ViewData["SecondaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.SecondaryOwnerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", task.TeamId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,CreatedOn,Priority,BusinessDT,Title,Description,IsCommissioned,PrimaryOwnerId,SecondaryOwnerId,ReviewerId,TeamId")] 
            Server.Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["PrimaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.PrimaryOwnerId);
            ViewData["ReviewerId"] = new SelectList(_context.Employees, "Id", "Id", task.ReviewerId);
            ViewData["SecondaryOwnerId"] = new SelectList(_context.Employees, "Id", "Id", task.SecondaryOwnerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", task.TeamId);
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.PrimaryOwner)
                .Include(t => t.Reviewer)
                .Include(t => t.SecondaryOwner)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
