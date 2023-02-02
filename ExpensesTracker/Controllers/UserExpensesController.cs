using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpensesTracker.Data;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpensesTracker.Controllers
{
    public class UserExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserExpenses
        public async Task<IActionResult> Index()
        {
              return View(await _context.UserExpense.ToListAsync());
        }

        // GET: UserExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserExpense == null)
            {
                return NotFound();
            }

            var userExpense = await _context.UserExpense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpense == null)
            {
                return NotFound();
            }

            return View(userExpense);
        }

        // GET: UserExpenses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,PlaceOfPurchase,AmountIncludingVAT,VAT,Reason,Members,Comment")] UserExpense userExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userExpense);
        }

        // GET: UserExpenses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserExpense == null)
            {
                return NotFound();
            }

            var userExpense = await _context.UserExpense.FindAsync(id);
            if (userExpense == null)
            {
                return NotFound();
            }
            return View(userExpense);
        }

        // POST: UserExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,PlaceOfPurchase,AmountIncludingVAT,VAT,Reason,Members,Comment")] UserExpense userExpense)
        {
            if (id != userExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExpenseExists(userExpense.Id))
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
            return View(userExpense);
        }

        // GET: UserExpenses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserExpense == null)
            {
                return NotFound();
            }

            var userExpense = await _context.UserExpense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpense == null)
            {
                return NotFound();
            }

            return View(userExpense);
        }

        // POST: UserExpenses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserExpense == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserExpense'  is null.");
            }
            var userExpense = await _context.UserExpense.FindAsync(id);
            if (userExpense != null)
            {
                _context.UserExpense.Remove(userExpense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExpenseExists(int id)
        {
          return _context.UserExpense.Any(e => e.Id == id);
        }
    }
}
