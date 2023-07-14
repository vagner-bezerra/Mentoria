using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using Microsoft.AspNetCore.Authorization;
using _1500MentoriaSistema.Models;

namespace _1500MentoriaSistema.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var data = await _context.People
                .Include(p => p.Circle)
                .ToListAsync();

            return View(data);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Circle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name");
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,CircleId,Name,CPF,Email,Phone,BornDate,NivelStudy,University,GraduateDate,Enterprise,Recommendation,IsStudying")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.RegisterDate = DateTime.Now;

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", person.CircleId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", person.CircleId);
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,CircleId,Name,CPF,Email,Phone,BornDate,NivelStudy,University,GraduateDate,Enterprise,Recommendation,IsStudying")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Busco no banco
                    var userPerson = await _context.People.FirstOrDefaultAsync(u => u.Id == id);

                    //Mapeo o dado antigo com o novo
                    userPerson.RegisterDate = DateTime.Now;
                    userPerson.Type = person.Type;
                    userPerson.CircleId = person.CircleId;
                    userPerson.Name = person.Name;
                    userPerson.CPF = person.CPF;
                    userPerson.Email = person.Email;
                    userPerson.Phone = person.Phone;
                    userPerson.BornDate = person.BornDate;
                    userPerson.NivelStudy = person.NivelStudy;
                    userPerson.University = person.University;
                    userPerson.GraduateDate = person.GraduateDate;
                    userPerson.Enterprise = person.Enterprise;
                    userPerson.Recommendation = person.Recommendation;
                    userPerson.IsStudying = person.IsStudying;

                    //Atualizo o dado antigo
                    _context.Update(userPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", person.CircleId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Circle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            var personFeedbacks = await _context.PeopleFeedback
                .Where(x => x.PersonId == id)
                .ToListAsync();
            var personLearning = await _context.PeopleLearning
                .Where(x => x.PersonId == id)
                .ToListAsync();

            _context.PeopleFeedback.RemoveRange(personFeedbacks);
            _context.PeopleLearning.RemoveRange(personLearning);
            _context.People.Remove(person);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
