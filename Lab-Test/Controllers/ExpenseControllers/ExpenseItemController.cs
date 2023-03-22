using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Service.IServices.IExpenseServices;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseItemController : Controller
    {
        #region Config

        private readonly IExpenseItemService _iService;
        public INotyfService NotifyService { get; }

        public ExpenseItemController(IExpenseItemService iService, INotyfService notifyService)
        {
            _iService = iService;
            NotifyService = notifyService;
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
            // List<ExpenseItem> models = new List<ExpenseItem>();
            // for (int i = 11001; i <= 20000; i++)
            // {
            //     var model = new ExpenseItem()
            //     {
            //         Id = 0,
            //         Name = "Test Item " + i,
            //         UnitPrice = 10 + i,
            //         Description = "Test Description" + i,
            //     };
            //     models.Add(model);
            // }
            //
            // var result = _iService.AddRange(models);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseItemDto dto)
        {
            if (!ModelState.IsValid) return null;
            var result = await _iService.AddAsync(dto);

            if (!result) return View(dto);
            NotifyService.Success("Expense item successfully saved!");
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
        public async Task<IActionResult> Edit(int id, ExpenseItemDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return null;

            var result = await _iService.UpdateAsync(dto);

            if (!result) return View(dto);
            NotifyService.Information("Expense item successfully updated!");
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
        public async Task<IActionResult> Search(DataTablePagination<ExpenseItemSearchDto> searchDto)
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
                NotifyService.Error("Expense item successfully deleted!");
                return RedirectToAction(nameof(Search));
            }

            var data = await _iService.GetByIdAsync(id);
            return View(data);
        }

        #endregion

        #region EXISTING CHECK

        [HttpPost]
        public async Task<IActionResult> IsExpItemNameExistAsync(string name, long id = 0)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(name))
                return BadRequest("Sorry, No Name Found!");
            var data = id > 0
                ? await _iService.GetFirstOrDefaultAsync(c => c.Name.ToUpper().Equals(name.ToUpper()) && c.Id != id)
                : await _iService.GetFirstOrDefaultAsync(c => c.Name.ToUpper().Equals(name.ToUpper()));
            return data != null ? Json(true) : Json(false);
        }

        // [HttpPost]
        // public JsonResult IsExpItemNameExistAsync(string name, int id = 0)
        // {
        //     var data = id > 0 ? _iService.GetFirstOrDefault(c => c.Name.ToUpper().Equals(name.ToUpper()) && c.Id != id) : _iService.GetFirstOrDefault(c => c.Name.ToUpper().Equals(name.ToUpper()));
        //     return data != null ? Json(true) : Json(false);
        // }

        #endregion
    }
}