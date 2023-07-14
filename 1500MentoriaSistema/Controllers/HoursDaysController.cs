using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using _1500MentoriaSistema.Data;
using _1500MentoriaSistema.Models;
using Microsoft.AspNetCore.Identity;

namespace _1500MentoriaSistema.Controllers
{
    [Authorize]
    public class HoursDaysController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public HoursDaysController(ApplicationDbContext context, UserManager<Person> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: HoursDays
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var isAdmin = (await _authorizationService.AuthorizeAsync(User, "AdminAccess")).Succeeded;

            if (isAdmin)
            {
                var data = await _context.HoursDay
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Project)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Person)
                .ThenInclude(h => h.Circle)
                .ToListAsync();

                return View("AdminIndex", data);
            }
            else
            {
                var data = await _context.HoursDay
                .Include(h => h.ActualState)
                .Where(h => h.ActualState.CircleId == user.CircleId)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Project)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Person)
                .ToListAsync();

                return View("Index", data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> HoursStates(int Id)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                var data = await _context.HoursDay
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Project)
                .Where(x => x.ActualState.Id == Id)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Person)
                .ThenInclude(h => h.Circle)
                .ToListAsync();

                return PartialView("_PartialIndex", data);
            }
            catch(Exception e)
            {

            }

            return BadRequest();
        }

        // GET: HoursDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursDay = await _context.HoursDay
                .Where(h => h.Id == id)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Project)
                .Include(h => h.ActualState)
                .ThenInclude(h => h.Person)
                .FirstOrDefaultAsync();

            if (hoursDay == null)
            {
                return NotFound();
            }

            return View(hoursDay);
        }

        // GET: HoursDays/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            var actualStates = _context.ActualStates
                .Where(x => x.Delivered == Delivered.NAO)
                .Include(x => x.Project);

            var stateItems = actualStates
                .Select(s => new
                {
                    s.Id,
                    s.Project,
                    s.Description,
                    s.Sprint,
                    s.TypeObject
                })
                .AsEnumerable()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Project.Name} - {s.TypeObject} - {s.Description} - {s.Sprint.ToString("dd/MM/yy")}"
                })
                .OrderBy(x => x.Text);

            ViewData["ActualstateId"] = stateItems;
            return View();
        }

        // POST: HoursDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActualstateId,InsertDate,Hours,Delivered")] HoursDay hoursDay)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                hoursDay.PersonId = user.Id;

                _context.Add(hoursDay);
                await _context.SaveChangesAsync();

                var actualState = await _context.ActualStates
                    .Where(x => x.Id == hoursDay.ActualstateId)
                    .Include(x => x.Project)
                    .Include(x => x.TypeConsultor)
                    .Include(x => x.HoursDay)
                    .FirstOrDefaultAsync();

                actualState.AttCalculos(true);

                _context.Update(actualState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var actualStates = _context.ActualStates
                .Where(x => x.Delivered == Delivered.NAO && x.CircleId == user.CircleId)
                .Include(x => x.Project);

            var stateItems = actualStates
                .Select(s => new
                {
                    s.Id,
                    s.Project,
                    s.Description,
                    s.Sprint,
                    s.TypeObject
                })
                .AsEnumerable()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Project.Name} - {s.TypeObject} - {s.Description} - {s.Sprint.ToString("dd/MM/yy")}"
                })
                .OrderBy(x => x.Text);

            ViewData["ActualstateId"] = stateItems;
            return View(hoursDay);
        }

        // GET: HoursDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursDay = await _context.HoursDay.FindAsync(id);
            if (hoursDay == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var actualStates = _context.ActualStates
                .Include(x => x.Project);

            var stateItems = actualStates
                .Select(s => new
                {
                    s.Id,
                    s.Project,
                    s.Description,
                    s.Sprint,
                    s.TypeObject
                })
                .AsEnumerable()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Project.Name} - {s.TypeObject} - {s.Description} - {s.Sprint.ToString("dd/MM/yy")}"
                })
                .OrderBy(x => x.Text);

            ViewData["ActualstateId"] = stateItems;
            return View(hoursDay);
        }

        // POST: HoursDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActualstateId,InsertDate,Hours,Delivered")] HoursDay hoursDay)
        {
            if (id != hoursDay.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                try
                {
                    var oldHoursDay = await _context.HoursDay
                            .AsNoTracking()
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();

                    oldHoursDay.ActualstateId = hoursDay.ActualstateId;
                    oldHoursDay.Hours = hoursDay.Hours;
                    oldHoursDay.InsertDate = hoursDay.InsertDate;
                    oldHoursDay.Delivered = hoursDay.Delivered;
                    oldHoursDay.PersonId = user.Id;

                    _context.Update(oldHoursDay);
                    await _context.SaveChangesAsync();

                    var actualState = await _context.ActualStates
                        .Where(x => x.Id == oldHoursDay.ActualstateId)
                        .Include(x => x.HoursDay)
                        .Include(x => x.Project)
                        .Include(x => x.TypeConsultor)
                        .FirstOrDefaultAsync();

                    actualState.AttCalculos(true);

                    actualState.UserId = user.Id;
                    _context.Update(actualState);
                    await _context.SaveChangesAsync();

                    actualState = await _context.ActualStates
                       .Where(x => x.Id == hoursDay.ActualstateId)
                       .Include(x => x.HoursDay)
                       .Include(x => x.Project)
                       .Include(x => x.TypeConsultor)
                       .FirstOrDefaultAsync();

                    actualState.AttCalculos(true);

                    actualState.UserId = user.Id;
                    _context.Update(actualState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoursDayExists(hoursDay.Id))
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

            var actualStates = _context.ActualStates
                .Include(x => x.Project);

            var stateItems = actualStates
                .Select(s => new
                {
                    s.Id,
                    s.Project,
                    s.Description,
                    s.Sprint,
                    s.TypeObject
                })
                .AsEnumerable()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Project.Name} - {s.TypeObject} - {s.Description} - {s.Sprint.ToString("dd/MM/yy")}"
                })
                .OrderBy(x => x.Text);

            ViewData["ActualstateId"] = stateItems;
            return View(hoursDay);
        }

        // GET: HoursDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursDay = await _context.HoursDay
                .Include(h => h.ActualState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoursDay == null)
            {
                return NotFound();
            }

            return View(hoursDay);
        }

        // POST: HoursDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoursDay = await _context.HoursDay.FindAsync(id);
            _context.HoursDay.Remove(hoursDay);
            await _context.SaveChangesAsync();

            var actualState = await _context.ActualStates
                .Where(x => x.Id == hoursDay.ActualstateId)
                .Include(x => x.HoursDay)
                .FirstOrDefaultAsync();

            var projeto = await _context.Project
                .Where(x => x.Id == actualState.ProjectId)
                .FirstOrDefaultAsync();

            var consultor = await _context.TypeConsultor
                .Where(x => x.Id == actualState.TypeConsultorId)
                .FirstOrDefaultAsync();

            actualState.Project = projeto;
            actualState.TypeConsultor = consultor;

            actualState.AttCalculos(true);
            _context.Update(actualState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoursDayExists(int id)
        {
            return _context.HoursDay.Any(e => e.Id == id);
        }
    }
}
