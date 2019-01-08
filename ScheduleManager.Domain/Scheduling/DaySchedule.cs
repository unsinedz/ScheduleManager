using System;
using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Scheduling
{
    public class DaySchedule : Entity
    {
        public virtual DateTime? DedicatedDate { get; set; }

        public virtual DayOfWeek DayOfWeek { get; set; }

        public virtual ActivityCollection Activities { get; set; }

        public struct Key : IEquatable<Key>
        {
            public Key(DayOfWeek dayOfWeek, DateTime? dedicatedDate = null)
            {
                this.DayOfWeek = dayOfWeek;
                this.DedicatedDate = dedicatedDate;
            }

            public DayOfWeek DayOfWeek { get; set; }

            public DateTime? DedicatedDate { get; set; }

            public override int GetHashCode()
            {
                var hash = this.DayOfWeek.GetHashCode();
                if (DedicatedDate.HasValue)
                    hash = hash * 17 + DedicatedDate.Value.GetHashCode();

                return hash;
            }

            public override bool Equals(object obj)
            {
                return obj is Key key && this.Equals(key);
            }

            public bool Equals(Key key)
            {
                return object.Equals(this, key.DayOfWeek) && object.Equals(this.DedicatedDate, key.DedicatedDate);
            }
        }
    }
}