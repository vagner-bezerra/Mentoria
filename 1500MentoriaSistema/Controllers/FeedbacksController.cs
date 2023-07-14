using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using _1500MentoriaSistema.Models;
using System.Security.Cryptography.X509Certificates;

namespace _1500MentoriaSistema.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var user = await _context.People
                .Where(x => x.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            var data = await _context.Feedbacks
                .Include(x => x.PersonFeedback)
                .Where(x => x.PersonFeedback.FirstOrDefault(y => y.PersonId == user.Id) != null)
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .Include(f => f.PersonFeedback)
                .ThenInclude(f => f.Person)
                .ToListAsync();

            return View(data);
        }


        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
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

        // POST: Feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment, StudentPersonId, TeacherPersonId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();

                PersonFeedback Student = new PersonFeedback();
                Student.TypePerson = TypePerson.Mentorado;
                Student.FeedbackId = feedback.Id;
                Student.PersonId = feedback.StudentPersonId;

                PersonFeedback Teacher = new PersonFeedback();
                Teacher.TypePerson = TypePerson.Mentor;
                Teacher.FeedbackId = feedback.Id;
                Teacher.PersonId = feedback.TeacherPersonId;

                _context.Add(Student);
                _context.Add(Teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);
            ViewData["StudentId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentorado), "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.People.Where(x => x.Type == TypePerson.Mentor), "Id", "Name");
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);

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
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CircleId,ThemeId,OportunityLearning,Note,Comment, StudentPersonId, TeacherPersonId")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldFeedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

                    oldFeedback.ThemeId = feedback.ThemeId;
                    oldFeedback.CircleId = feedback.CircleId;
                    oldFeedback.Comment = feedback.Comment;
                    oldFeedback.Note = feedback.Note;
                    oldFeedback.Date = feedback.Date;
                    oldFeedback.OportunityLearning = feedback.OportunityLearning;

                    var feedbacks = await _context.PeopleFeedback
                        .Where(x => x.FeedbackId == feedback.Id)
                        .ToListAsync();

                    foreach (var i in feedbacks)
                    {
                        if (i.TypePerson == TypePerson.Mentor)
                        {
                            i.PersonId = feedback.TeacherPersonId;
                        }
                        else
                        {
                            i.PersonId = feedback.StudentPersonId;
                        }
                    }

                    _context.Update(oldFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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

            ViewData["CircleId"] = new SelectList(_context.Circle, "Id", "Name", feedback.CircleId);
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name", feedback.ThemeId);
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
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Circle)
                .Include(f => f.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
