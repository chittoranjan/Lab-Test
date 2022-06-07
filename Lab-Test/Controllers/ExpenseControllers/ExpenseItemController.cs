using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ProjectDbContext;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseItemController : Controller
    {
        private readonly LabTestDbContext _context;

        public ExpenseItemController(LabTestDbContext context)
        {
            _context = context;
        }

        // GET: ExpenseItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseItems.ToListAsync());
        }

        // GET: ExpenseItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // GET: ExpenseItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice,Description")] ExpenseItem expenseItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseItem);
        }

        // GET: ExpenseItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItems.FindAsync(id);
            if (expenseItem == null)
            {
                return NotFound();
            }
            return View(expenseItem);
        }

        // POST: ExpenseItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitPrice,Description")] ExpenseItem expenseItem)
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
                    if (!ExpenseItemExists(expenseItem.Id))
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

        // GET: ExpenseItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // POST: ExpenseItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseItem = await _context.ExpenseItems.FindAsync(id);
            _context.ExpenseItems.Remove(expenseItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseItemExists(int id)
        {
            return _context.ExpenseItems.Any(e => e.Id == id);
        }
    }
}
