using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Resolver.DistributedRedisCache;
using Service.IServices.IExpenseServices;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Test.Controllers.ExpenseControllers
{
    public class ExpenseController : Controller
    {
        #region Config

        private readonly IExpenseService _iService;
        private readonly IExpenseItemService _iExpItemService;
        public readonly IDistributedCache _iDistributedCache;
        public INotyfService NotifyService { get; }

        public ExpenseController(IExpenseService iService, INotyfService iNotifyService, IExpenseItemService iExpItemService, IDistributedCache iDistributedCache)
        {
            _iService = iService;
            NotifyService = iNotifyService;
            _iExpItemService = iExpItemService;
            _iDistributedCache = iDistributedCache;
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

        public async Task<IActionResult> Create()
        {
            var cacheService = new DistributedRedisCacheService(_iDistributedCache);
            var expItemData = await cacheService.GetStringAsync(CacheKeyName.ExpenseItem.ToString());

            if (expItemData.Count <= 0)
            {
                var expItemSelectionList = await _iExpItemService.GetSelectionListAsync();

                expItemData = expItemSelectionList.ToList<object>();
                var result = await cacheService.SetStringAsync(CacheKeyName.ExpenseItem.ToString(), expItemData);
            }

            ViewBag.ExpItemSelectionList = expItemData;
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

            var expItemSelectionList = (await _iExpItemService.GetAllAsync()).ToList();
            expItemSelectionList.Insert(0, new ExpenseItem() { Id = 0, Name = "Select Item" });
            ViewBag.ExpItemSelectionList = expItemSelectionList;

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