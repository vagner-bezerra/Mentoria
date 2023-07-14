using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using _1500MentoriaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace _1500MentoriaSistema.Controllers
{
    public class LearningsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public LearningsController(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: Learnings
        public async Task<IActionResult> Index()
        {
            var user = await _context.People
                .Where(x => x.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            var data = await _context.Learnings
                .Include(x => x.PersonLearning)
                .Where(x => x.PersonLearning.FirstOrDefault(y => y.PersonId == user.Id) != null)
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .Include(l => l.PersonLearning)
                .ThenInclude(l => l.Person)
                .ToListAsync();

            return View(data);
        }

        // GET: Learnings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // GET: Learnings/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");

            var people = await _context.People.ToListAsync();
            var teachers = people.Where(x => x.Type == TypePerson.Mentor).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });
            var students = people.Where(x => x.Type == TypePerson.Mentorado).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            ViewData["StudentId"] = students;
            ViewData["TeacherId"] = teachers;
            return View();
        }

        // POST: Learnings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate,MeasurementForm,Result,Comment,Status,TeacherPersonId, StudentPersonId")] Learning learning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learning);
                await _context.SaveChangesAsync();

                PersonLearning Student = new PersonLearning();
                Student.TypePerson = TypePerson.Mentorado;
                Student.LearningId = learning.Id;
                Student.PersonId = learning.StudentPersonId;

                PersonLearning Teacher = new PersonLearning();
                Teacher.TypePerson = TypePerson.Mentor;
                Teacher.LearningId = learning.Id;
                Teacher.PersonId = learning.TeacherPersonId;

                _context.Add(Student);
                _context.Add(Teacher);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            var people = await _context.People.ToListAsync();

            var teachers = people.Where(x => x.Type == TypePerson.Mentor).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            var students = people.Where(x => x.Type == TypePerson.Mentorado).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            ViewData["StudentId"] = students;
            ViewData["TeacherId"] = teachers;
            return View(learning);
        }

        // GET: Learnings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings.FindAsync(id);
            if (learning == null)
            {
                return NotFound();
            }

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            var people = await _context.People.ToListAsync();

            var teachers = people.Where(x => x.Type == TypePerson.Mentor).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            var students = people.Where(x => x.Type == TypePerson.Mentorado).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            ViewData["StudentId"] = students;
            ViewData["TeacherId"] = teachers;
            return View(learning);
        }

        // POST: Learnings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CircleId,ThemeId,OportunityLearning,LearningAction,MeasurementDate,MeasurementForm,Result,Comment,Status, StudentPersonId, TeacherPersonId")] Learning learning)
        {
            if (id != learning.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldLearning = await _context.Learnings.FirstOrDefaultAsync(x => x.Id == id);

                    oldLearning.CircleId = learning.CircleId;
                    oldLearning.ThemeId = learning.ThemeId;
                    oldLearning.Comment = learning.Comment;
                    oldLearning.OportunityLearning = learning.OportunityLearning;
                    oldLearning.LearningAction = learning.LearningAction;
                    oldLearning.MeasurementDate = learning.MeasurementDate;
                    oldLearning.MeasurementForm = learning.MeasurementForm;
                    oldLearning.Result = learning.Result;
                    oldLearning.Status = learning.Status;

                    var aprendizados = await _context.PeopleLearning
                        .Where(x => x.LearningId == learning.Id)
                        .ToListAsync();

                    foreach (var i in aprendizados)
                    {
                        if (i.TypePerson == TypePerson.Mentor)
                        {
                            i.PersonId = learning.TeacherPersonId;
                        }
                        else
                        {
                            i.PersonId = learning.StudentPersonId;
                        }
                    }

                    _context.Update(oldLearning); // Um dado do banco
                    _context.UpdateRange(aprendizados); // Varios dados do bancos
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningExists(learning.Id))
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

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", learning.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", learning.ThemeId);
            var people = await _context.People.ToListAsync();

            var teachers = people.Where(x => x.Type == TypePerson.Mentor).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            var students = people.Where(x => x.Type == TypePerson.Mentorado).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
            });

            ViewData["StudentId"] = students;
            ViewData["TeacherId"] = teachers;
            return View(learning);
        }

        // GET: Learnings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learning = await _context.Learnings
                .Include(l => l.Circle)
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learning == null)
            {
                return NotFound();
            }

            return View(learning);
        }

        // POST: Learnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learning = await _context.Learnings.FindAsync(id);
            _context.Learnings.Remove(learning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningExists(int id)
        {
            return _context.Learnings.Any(e => e.Id == id);
        }
    }
}
