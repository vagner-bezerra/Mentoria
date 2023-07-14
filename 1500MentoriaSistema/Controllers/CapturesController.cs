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
    public class CapturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Captures
        public async Task<IActionResult> Index()
        {
            var data = await _context.Captures
                .ToListAsync();

            return View(data);
        }

        // GET: Captures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captures = await _context.Captures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (captures == null)
            {
                return NotFound();
            }

            return View(captures);
        }

        // GET: Captures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Captures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Captures captures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(captures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(captures);
        }

        // GET: Captures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captures = await _context.Captures.FindAsync(id);
            if (captures == null)
            {
                return NotFound();
            }
            return View(captures);
        }

        // POST: Captures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Captures captures)
        {
            if (id != captures.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(captures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CapturesExists(captures.Id))
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
            return View(captures);
        }

        // GET: Captures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captures = await _context.Captures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (captures == null)
            {
                return NotFound();
            }

            return View(captures);
        }

        // POST: Captures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var captures = await _context.Captures.FindAsync(id);
            _context.Captures.Remove(captures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CapturesExists(int id)
        {
            return _context.Captures.Any(e => e.Id == id);
        }
    }
}
