using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Model.DataTableModels;
using Model.DtoModels.ExpenseDtoModels;
using Service.IServices.IExpenseServices;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseItemController : Controller
    {
        private readonly IExpenseItemService _iService;
        public INotyfService NotifyService { get; }
        public ExpenseItemController(IExpenseItemService iService, INotyfService notifyService)
        {
            _iService = iService;
            NotifyService = notifyService;
        }

        // GET: ExpenseItem
        public async Task<IActionResult> Index()
        {
            var data = await _iService.GetAllAsync();
            return View(data);
        }

        // GET: ExpenseItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var data = await _iService.GetByIdAsync(id ?? 0);
            if (data == null) return NotFound();
            return View(data);
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
        public async Task<IActionResult> Create(ExpenseItemDto dto)
        {
            if (!ModelState.IsValid) return null;
            var result = await _iService.AddAsync(dto);

            if (!result) return View(dto);
            NotifyService.Success("Expense item successfully saved!");
            return RedirectToAction(nameof(Index));
        }

        // GET: ExpenseItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var data = await _iService.GetByIdAsync(id ?? 0);
            if (data == null) return NotFound();

            return View(data);
        }

        // POST: ExpenseItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseItemDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return null;

            var result = await _iService.UpdateAsync(dto);

            if (!result) return View(dto);
            NotifyService.Information("Expense item successfully updated!");
            return RedirectToAction(nameof(Index));
        }

        // [HttpGet("Search")]
        // public async Task<IActionResult> Search(DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto> searchVm = null)
        public async Task<IActionResult> Search()
        {
            // searchVm ??= new DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto>();
            DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto> searchVm = new DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto>();
            if (searchVm?.SearchModel == null) searchVm.SearchModel = new ExpenseItemSearchDto();
            var dataTable = await _iService.Search(searchVm);

            return Ok(dataTable);
        }

        // GET: ExpenseItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var data = await _iService.GetByIdAsync(id ?? 0);
            if (data == null) return NotFound();
            return View(data);
        }

        // POST: ExpenseItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _iService.DeleteAsync(id);
            if (result)
            {
                NotifyService.Error("Expense item successfully deleted!");
                return RedirectToAction(nameof(Index));
            }

            var data = await _iService.GetByIdAsync(id);
            return View(data);
        }

        // [HttpGet("IsExpenseItemNameExist", Name = "IsExpenseItemNameExist")]
        // public async Task<IActionResult> IsExpItemCategoryNameExistAsync(string name, long id = 0)
        // {
        //     if (string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(name)) return BadRequest("Sorry, No Name Found!");
        //     var data = id > 0 ? await _iService.IsExistsAsync(c => c.Name.Equals(name) && c.Id != id) : await _iService.IsExistsAsync(c => c.Name.Equals(name));
        //     return Ok(data);
        // }
    }
}
