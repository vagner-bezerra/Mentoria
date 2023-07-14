using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using _1500MentoriaSistema.Models;

namespace _1500MentoriaSistema.Controllers
{
    public class CirclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CirclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Circles
        public async Task<IActionResult> Index()
        {
            var data = await _context.Circle.ToListAsync();

            return View(data);
        }

        // GET: Circles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // GET: Circles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Circles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Circle circle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(circle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(circle);
        }

        // GET: Circles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circle.FindAsync(id);
            if (circle == null)
            {
                return NotFound();
            }
            return View(circle);
        }

        // POST: Circles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Circle circle)
        {
            if (id != circle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldCircle = await _context.Circle.FirstOrDefaultAsync(x => x.Id == id);

                    oldCircle.Name = circle.Name;

                    _context.Update(oldCircle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircleExists(circle.Id))
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
            return View(circle);
        }

        // GET: Circles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = await _context.Circle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var circle = await _context.Circle.FindAsync(id);
            _context.Circle.Remove(circle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("MultipleDelete")]
        public async Task<IActionResult> MultipleDeleteConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var circle = await _context.Circle.FindAsync(id);
                    _context.Circle.Remove(circle);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool CircleExists(int id)
        {
            return _context.Circle.Any(e => e.Id == id);
        }
    }
}
