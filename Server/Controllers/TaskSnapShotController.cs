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
    public class TaskSnapShotController : Controller
    {
        private readonly WLMDbContext _context;

        public TaskSnapShotController(WLMDbContext context)
        {
            _context = context;
        }

        // GET: TaskSnapShot
        public async Task<IActionResult> Index()
        {
            var wLMDbContext = _context.TaskSnapShots.Include(t => t.AssignedBy).Include(t => t.AssignedTo).Include(t => t.Task);
            return View(await wLMDbContext.ToListAsync());
        }

        // GET: TaskSnapShot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskSnapShot = await _context.TaskSnapShots
                .Include(t => t.AssignedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskSnapShot == null)
            {
                return NotFound();
            }

            return View(taskSnapShot);
        }

        // GET: TaskSnapShot/Create
        public IActionResult Create()
        {
            ViewData["AssignedById"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["AssignedToId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id");
            return View();
        }

        // POST: TaskSnapShot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,Status,PercentageOfWorkCompleted,AssignedToId,AssignedById,TaskId")] TaskSnapShot taskSnapShot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskSnapShot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedById"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedById);
            ViewData["AssignedToId"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedToId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", taskSnapShot.TaskId);
            return View(taskSnapShot);
        }

        // GET: TaskSnapShot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskSnapShot = await _context.TaskSnapShots.FindAsync(id);
            if (taskSnapShot == null)
            {
                return NotFound();
            }
            ViewData["AssignedById"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedById);
            ViewData["AssignedToId"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedToId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", taskSnapShot.TaskId);
            return View(taskSnapShot);
        }

        // POST: TaskSnapShot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStamp,Status,PercentageOfWorkCompleted,AssignedToId,AssignedById,TaskId")] TaskSnapShot taskSnapShot)
        {
            if (id != taskSnapShot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskSnapShot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskSnapShotExists(taskSnapShot.Id))
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
            ViewData["AssignedById"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedById);
            ViewData["AssignedToId"] = new SelectList(_context.Employees, "Id", "Id", taskSnapShot.AssignedToId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", taskSnapShot.TaskId);
            return View(taskSnapShot);
        }

        // GET: TaskSnapShot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskSnapShot = await _context.TaskSnapShots
                .Include(t => t.AssignedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskSnapShot == null)
            {
                return NotFound();
            }

            return View(taskSnapShot);
        }

        // POST: TaskSnapShot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskSnapShot = await _context.TaskSnapShots.FindAsync(id);
            _context.TaskSnapShots.Remove(taskSnapShot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskSnapShotExists(int id)
        {
            return _context.TaskSnapShots.Any(e => e.Id == id);
        }
    }
}
