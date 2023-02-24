using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ProjectDbContext;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseController : Controller
    {
        private readonly LabTestDbContext _context;

        public ExpenseController(LabTestDbContext context)
        {
            _context = context;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expenses.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.Expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice,Description")] Expense expenseItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseItem);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.Expenses.FindAsync(id);
            if (expenseItem == null)
            {
                return NotFound();
            }
            return View(expenseItem);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitPrice,Description")] Expense expenseItem)
        {
            if (id != expenseItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expenseItem.Id))
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
            return View(expenseItem);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.Expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null) _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
