using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers.Api
{
    [Area("Api")]
    public class ItemsController<TItem> : Controller
    {
        protected IAsyncProvider<TItem> ItemProvider { get; private set; }

        public ItemsController(IAsyncProvider<TItem> itemProvider)
        {
            ItemProvider = itemProvider;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List([PageIndex]int page, int pageSize)
        {
            var items = await ItemProvider.ListAsync(new Specification<TItem>()
                .WithPageSize(pageSize)
                .WithPageIndex(page));
            return Ok(items);
        }

        [HttpGet]
        [ActionName("Item")]
        public virtual async Task<IActionResult> GetItem(Guid id)
        {
            if (!IsValidId(id))
                return BadRequest();

            var item = await ItemProvider.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        protected virtual bool IsValidId(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}