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
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var expanses = await _context.Expenses.ToListAsync();
            return View(expanses);
        }

        public async Task<IActionResult> Duplicate(List<int> ids, string type, int count)
        {
            try
            {
                foreach (var id in ids)
                {
                    var expenses = await _context.Expenses.FindAsync(id);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.BankAccount)
                .Include(e => e.Capture)
                .Include(e => e.CostCenter)
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name");
            ViewData["CaptureId"] = new SelectList(_context.Captures, "Id", "Name");
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name");
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,RegisterDate,CashDate,MonthDate,CompeteDate,CompeteMonthDate,CostCenterId,CaptureId,PersonId,BankAccountId,TargetBill,Description,Value,Validated,Plan,BankValueMoment,EnterpriseValueMoment")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.MonthDate = expense.CashDate;
                expense.CompeteMonthDate = expense.CompeteDate;

                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", expense.BankAccountId);
            ViewData["CaptureId"] = new SelectList(_context.Captures, "Id", "Name", expense.CaptureId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", expense.CostCenterId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", expense.PersonId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", expense.BankAccountId);
            ViewData["CaptureId"] = new SelectList(_context.Captures, "Id", "Name", expense.CaptureId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", expense.CostCenterId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", expense.PersonId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,RegisterDate,CashDate,MonthDate,CompeteDate,CompeteMonthDate,CostCenterId,CaptureId,PersonId,BankAccountId,TargetBill,Description,Value,Validated,Plan,BankValueMoment,EnterpriseValueMoment")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expense.MonthDate = expense.CashDate;
                    expense.CompeteMonthDate = expense.CompeteDate;

                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
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
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "Name", expense.BankAccountId);
            ViewData["CaptureId"] = new SelectList(_context.Captures, "Id", "Name", expense.CaptureId);
            ViewData["CostCenterId"] = new SelectList(_context.CostCenter, "Id", "Name", expense.CostCenterId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Name", expense.PersonId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.BankAccount)
                .Include(e => e.Capture)
                .Include(e => e.CostCenter)
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
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
                    var expenses = await _context.Expenses.FindAsync(id);
                    _context.Expenses.Remove(expenses);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
