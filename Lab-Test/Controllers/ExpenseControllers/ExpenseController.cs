using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Service.IServices.IExpenseServices;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseController : Controller
    {
        #region Config
        private readonly IExpenseService _iService;
        public INotyfService NotifyService { get; }
        public ExpenseController(IExpenseService iService, INotyfService iNotifyService)
        {
            _iService = iService;
            NotifyService = iNotifyService;
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var data = await _iService.GetByIdAsync(id ?? 0);
            if (data == null) return NotFound();
            return View(data);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseDto dto)
        {
            if (!ModelState.IsValid) return null;
            var result = await _iService.AddAsync(dto);

            if (!result) return View(dto);
            NotifyService.Success("Expense successfully saved!");
            return RedirectToAction(nameof(Search));
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var data = await _iService.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return null;

            var result = await _iService.UpdateAsync(dto);

            if (!result) return View(dto);
            NotifyService.Information("Expense successfully updated!");
            return RedirectToAction(nameof(Search));
        }
        #endregion

        #region Search
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(DataTablePagination<ExpenseSearchDto> searchDto)
        {
            var dataTable = await _iService.Search(searchDto);
            return Json(dataTable);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var data = await _iService.GetByIdAsync(id ?? 0);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _iService.DeleteAsync(id);
            if (result)
            {
                NotifyService.Error("Expense successfully deleted!");
                return RedirectToAction(nameof(Search));
            }

            var data = await _iService.GetByIdAsync(id);
            return View(data);
        }
        #endregion
    }
}
