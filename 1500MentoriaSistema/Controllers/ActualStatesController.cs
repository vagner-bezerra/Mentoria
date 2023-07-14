using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using _1500MentoriaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _1500MentoriaSistema.Controllers
{
    public class ActualStatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ActualStatesController(ApplicationDbContext context, UserManager<Person> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: ActualStates
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var isAdmin = (await _authorizationService.AuthorizeAsync(User, "AdminAccess")).Succeeded;

            if (isAdmin)
            {
                var data = await _context.ActualStates
                    .OrderByDescending(a => a.Sprint)
                    .Include(a => a.Circle)
                    .Include(a => a.Person)
                    .Include(a => a.Project)
                    .ToListAsync();

                return View("AdminIndex", data);
            }
            else
            {
                var data = await _context.ActualStates
                    .Where(x => x.CircleId == user.CircleId)
                    .OrderByDescending(a => a.Sprint)
                    .Include(a => a.Circle)
                    .Include(a => a.Person)
                    .Include(a => a.Project)
                    .ToListAsync();

                return View("Index", data);
            }
        }

        public async Task<IActionResult> FilterIndex(ActualState filter)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAdmin = (await _authorizationService.AuthorizeAsync(User, "AdminAccess")).Succeeded;
            var filteredList = new List<ActualState>();

            if (isAdmin)
            {
                filteredList = await _context.ActualStates
                    .OrderByDescending(a => a.Sprint)
                    .Include(a => a.Circle)
                    .Include(a => a.Person)
                    .Include(a => a.Project)
                    .ToListAsync();
            }
            else
            {
                filteredList = await _context.ActualStates
                    .Where(x => x.CircleId == user.CircleId)
                    .OrderByDescending(a => a.Sprint)
                    .Include(a => a.Circle)
                    .Include(a => a.Person)
                    .Include(a => a.Project)
                    .ToListAsync();
            }

            if (filter.CircleId != 0)
            {
                filteredList = filteredList.Where(a => a.CircleId == filter.CircleId).ToList();
            }

            if (filter.ProjectId != 0)
            {
                filteredList = filteredList.Where(a => a.ProjectId == filter.ProjectId).ToList();
            }

            if (filter.TypeObject != TypeObject.None)
            {
                filteredList = filteredList.Where(a => a.TypeObject == filter.TypeObject).ToList();
            }

            if (filter.TypeConsultorId != 0)
            {
                filteredList = filteredList.Where(a => a.TypeConsultorId == filter.TypeConsultorId).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                filteredList = filteredList.Where(a => a.Description == filter.Description).ToList();
            }

            if (filter.TimePlanned != 0)
            {
                filteredList = filteredList.Where(a => a.TimePlanned == filter.TimePlanned).ToList();
            }

            if (filter.PersonId != 0)
            {
                filteredList = filteredList.Where(a => a.PersonId == filter.PersonId).ToList();
            }

            if(filter.RealTime != 0)
            {
                filteredList = filteredList.Where(a => a.RealTime == filter.RealTime).ToList();
            }

            if (filter.Delivered != Delivered.VAZIO)
            {
                filteredList = filteredList.Where(a => a.Delivered == filter.Delivered).ToList();
            }

            if (filter.Productivity != 0)
            {
                filteredList = filteredList.Where(a => a.Productivity == filter.Productivity).ToList();
            }

            if (filter.Sprint != DateTime.MinValue)
            {
                filteredList = filteredList.Where(a => a.Sprint == filter.Sprint).ToList();
            }

            if (filter.FinalValue != 0)
            {
                filteredList = filteredList.Where(a => a.FinalValue == filter.FinalValue).ToList();
            }

            if (filter.UserId != 0)
            {
                filteredList = filteredList.Where(a => a.UserId == filter.UserId).ToList();
            }

            return View(filteredList);
        }

        // GET: ActualStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualState = await _context.ActualStates
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (actualState == null)
            {
                return NotFound();
            }

            return View(actualState);
        }

        // GET: ActualStates/Create
        public IActionResult Create()
        {
            ViewData["CircleId"] = new SelectList(_context.Circle.OrderBy(x => x.Name), "Id", "Name");
            
            ViewData["ProjectId"] = new SelectList(_context.Project.OrderBy(x => x.Name), "Id", "Name");
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultor.OrderBy(x => x.Name), "Id", "Name");

            var consultors = _context.People
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
                })
                .OrderBy(x => x.Text)
                .ToList();

            ViewData["PersonId"] = consultors;
            return View();
        }

        // POST: ActualStates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CircleId,ProjectId,TypeObject,TypeConsultorId,Description,TimePlanned,PersonId,Sprint,Delivered")] ActualState actualState)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var projeto = await _context.Project
                    .Where(x => x.Id == actualState.ProjectId)
                    .FirstOrDefaultAsync();

                var consultor = await _context.TypeConsultor
                    .Where(x => x.Id == actualState.TypeConsultorId)
                    .FirstOrDefaultAsync();

                actualState.Project = projeto;
                actualState.TypeConsultor = consultor;

                actualState.AttCalculos(false);

                actualState.UserId = user.Id;

                _context.ActualStates.Add(actualState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CircleId"] = new SelectList(_context.Circle.OrderBy(x => x.Name), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Project.OrderBy(x => x.Name), "Id", "Name");
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultor.OrderBy(x => x.Name), "Id", "Name");

            var consultors = _context.People
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
                })
                .OrderBy(x => x.Text)
                .ToList();

            ViewData["PersonId"] = consultors;
            return View(actualState);
        }

        // GET: ActualStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualState = await _context.ActualStates.FindAsync(id);
            if (actualState == null)
            {
                return NotFound();
            }
            ViewData["CircleId"] = new SelectList(_context.Circle.OrderBy(x => x.Name), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Project.OrderBy(x => x.Name), "Id", "Name");
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultor.OrderBy(x => x.Name), "Id", "Name");

            var consultors = _context.People
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
                })
                .OrderBy(x => x.Text)
                .ToList();

            ViewData["PersonId"] = consultors;
            return View(actualState);
        }

        // POST: ActualStates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CircleId,ProjectId,TypeObject,TypeConsultorId,Description,TimePlanned,Delivered,PersonId,Sprint")] ActualState actualState)
        {
            if (id != actualState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var state = await _context.ActualStates.FirstOrDefaultAsync(x => x.Id == id);

                    var projeto = await _context.Project
                        .Where(x => x.Id == actualState.ProjectId)
                        .FirstOrDefaultAsync();

                    var consultor = await _context.TypeConsultor
                        .Where(x => x.Id == actualState.TypeConsultorId)
                        .FirstOrDefaultAsync();

                    var hoursDay = await _context.HoursDay
                        .Where(x => x.ActualstateId == actualState.Id)
                        .ToListAsync();

                    actualState.HoursDay = hoursDay;
                    actualState.Project = projeto;
                    actualState.TypeConsultor = consultor;

                    actualState.AttCalculos(false);

                    state.ProjectId = actualState.ProjectId;
                    state.CircleId = actualState.CircleId;
                    state.PersonId = actualState.PersonId;
                    state.TypeObject = actualState.TypeObject;
                    state.TypeConsultorId = actualState.TypeConsultorId;
                    state.Description = actualState.Description;
                    state.TimePlanned = actualState.TimePlanned;
                    state.RealTime = actualState.RealTime;
                    state.Delivered = actualState.Delivered;
                    state.Productivity = actualState.Productivity;
                    state.Sprint = actualState.Sprint;
                    state.Value = actualState.Value;
                    state.FinalValue = actualState.FinalValue;
                    state.UserId = user.Id;

                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActualStateExists(actualState.Id))
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
            ViewData["CircleId"] = new SelectList(_context.Circle.OrderBy(x => x.Name), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Project.OrderBy(x => x.Name), "Id", "Name");
            ViewData["TypeConsultorId"] = new SelectList(_context.TypeConsultor.OrderBy(x => x.Name), "Id", "Name");

            var consultors = _context.People
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = string.IsNullOrEmpty(s.Name) ? s.UserName : s.Name,
                })
                .OrderBy(x => x.Text)
                .ToList();

            ViewData["PersonId"] = consultors;
            return View(actualState);
        }

        // GET: ActualStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualState = await _context.ActualStates
                .Include(a => a.Circle)
                .Include(a => a.Person)
                .Include(a => a.Project)
                .Include(a => a.TypeConsultor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualState == null)
            {
                return NotFound();
            }

            return View(actualState);
        }

        // POST: ActualStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actualState = await _context.ActualStates.FindAsync(id);
            _context.ActualStates.Remove(actualState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("MultipleDelete")]
        public async Task<IActionResult> MultipleDeleteConfirmed(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var actualState = await _context.ActualStates.FindAsync(id);
                    _context.ActualStates.Remove(actualState);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ActualStateExists(int id)
        {
            return _context.ActualStates.Any(e => e.Id == id);
        }
    }
}
