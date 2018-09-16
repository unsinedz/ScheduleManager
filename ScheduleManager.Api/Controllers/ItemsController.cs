using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Models;
using ScheduleManager.Api.Models.Editors;
using ScheduleManager.Domain.DependencyInjection;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    public class ItemsController<TItem, TItemViewModel> : Controller
        where TItem : Entity, new()
        where TItemViewModel : ItemViewModel<TItem>
    {
        protected IAsyncProvider<TItem> ItemProvider { get; set; }

        public ItemsController(IAsyncProvider<TItem> itemProvider)
        {
            this.ItemProvider = itemProvider;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var items = (await ItemProvider.ListAsync()).Select(CreateItemViewModel);
            object model = items.Any(x => x.PreviewableInList)
                ? CreateListModel(items.OfType<IPreviewableItemModel>())
                : (object)items;
            return View(model);
        }

        [HttpGet]
        [ActionName("Create")]
        public virtual IActionResult CreateGet()
        {
            var model = CreateEmptyModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public virtual async Task<IActionResult> CreatePost()
        {
            var model = CreateEmptyModel();
            await TryUpdateModelAsync(model);
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
        [ActionName("Edit")]
        public virtual async Task<IActionResult> EditGet(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var item = await ItemProvider.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var model = CreateEmptyModel();
            model.Initialize(item);
            return View(model: model);
        }

        [HttpPost]
        [ActionName("Edit")]
        public virtual async Task<IActionResult> EditPost(Guid id)
        {
            var model = CreateEmptyModel();
            await TryUpdateModelAsync(model);
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
            var updated = await model.TryUpdateEntityProperties(item);
            if (updated)
                await ItemProvider.CreateAsync(item);

            return updated;
        }

        protected virtual TItemViewModel CreateEmptyModel()
        {
            return TypeResolver.Resolve<TItemViewModel>();
        }

        protected virtual TItemViewModel CreateItemViewModel(TItem item)
        {
            var model = CreateEmptyModel();
            model.Initialize(item);
            return model;
        }

        protected virtual PreviewableItemsViewModel CreateListModel(IEnumerable<IPreviewableItemModel> previewableItems)
        {
            return new PreviewableItemsViewModel
            {
                Title = GetListTitle(),
                PreviewableItems = previewableItems,
                EditUrl = new Func<object, string>(routeData => Url.Action("Edit", this.ControllerContext.ActionDescriptor.ControllerName, routeData)),
                DeleteUrl = new Func<object, string>(routeData => Url.Action("Delete", this.ControllerContext.ActionDescriptor.ControllerName, routeData)),
            };
        }

        protected virtual string GetListTitle() => "Items";
    }
}