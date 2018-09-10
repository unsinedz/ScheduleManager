using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Models;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    public class ItemsController<TItem, TItemViewModel> : Controller
        where TItem : Entity, new()
        where TItemViewModel : ItemViewModel<TItem>, new()
    {
        protected IAsyncProvider<TItem> ItemProvider { get; set; }

        public ItemsController(IAsyncProvider<TItem> itemProvider)
        {
            this.ItemProvider = itemProvider;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var items = await ItemProvider.ListAsync();
            return View(items.Select(CreateItemViewModel));
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            return View(new TItemViewModel());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model: model);

            var item = new TItem();
            this.OnBeforeCreate(item, model);
            if (await TrySaveItemAsync(new TItem(), model))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, "Item creation failed.");
            return View(model: model);
        }

        protected virtual void OnBeforeCreate(TItem item, TItemViewModel model)
        {
            if (item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var item = await ItemProvider.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var model = new TItemViewModel();
            model.Initialize(item);
            return View(model: model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(TItemViewModel model, Guid id)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
                return View(model: model);

            var item = await ItemProvider.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            if (await TrySaveItemAsync(item, model))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, "Item edition failed.");
            return View(model: model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return Json(1);

            var item = await ItemProvider.GetByIdAsync(id);
            if (item == null)
                return Json(1);

            await ItemProvider.RemoveAsync(item);
            return Json(0);
        }

        protected virtual async Task<bool> TrySaveItemAsync(TItem item, TItemViewModel model)
        {
            var updated = model.TryUpdateEntity(item);
            if (updated)
                await ItemProvider.CreateAsync(item);

            return updated;
        }

        protected virtual TItemViewModel CreateItemViewModel(TItem item)
        {
            var model = new TItemViewModel();
            model.Initialize(item);
            return model;
        }
    }
}