using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ScheduleManager.Api.Models.Scheduling;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Scheduling;
using ScheduleManager.Localizations;

namespace ScheduleManager.Api.Controllers.Api.V1.Scheduling
{
    [Area("Api_V1")]
    public class DayScheduleController : ItemsController<DaySchedule>
    {
        private readonly IStringLocalizer _stringLocalizer;

        public DayScheduleController(IAsyncProvider<DaySchedule> itemProvider, IStringLocalizer stringLocalizer) : base(itemProvider)
        {
            this._stringLocalizer = stringLocalizer;
        }

        protected override object DecorateItem(DaySchedule item) => new DayScheduleWrapper(item, _stringLocalizer);

        public class DayScheduleWrapper
        {
            private readonly DaySchedule _item;
            private readonly IStringLocalizer _stringLocalizer;

            public DayScheduleWrapper(DaySchedule item, IStringLocalizer stringLocalizer)
            {
                this._item = item;
                this._stringLocalizer = stringLocalizer;
            }

            public virtual Guid Id => _item.Id;

            public virtual DateTime? DedicatedDate => _item.DedicatedDate;

            public virtual DayOfWeek DayOfWeek => _item.DayOfWeek;

            public virtual string DayOfWeekString => _stringLocalizer.LocalizeEnumValue(this.DayOfWeek);

            public virtual ActivityCollection Activities => _item.Activities;
        }
    }
}