var Framework = Framework || {};
(function () {
    if (Framework.Autocomplete) {
        Framework.Autocomplete.addTypeMappers(['faculty', 'department', 'course', 'room', 'subject', 'activity'], function (data) {
            var autocompleteData = {};
            if (Array.isArray(data)) {
                $.each(data, function () {
                    if (this.title)
                        autocompleteData[this.title] = this;
                });
            }
            else if (data)
                return { key: data.title, data: data };

            return autocompleteData;
        }, function (entity) {
            return {
                title: entity.title,
                value: entity.id
            };
        }, '.Id');

        Framework.Autocomplete.addTypeMappers(['lecturer', 'attendee'], function (data) {
            var autocompleteData = {};
            if (Array.isArray(data)) {
                $.each(data, function () {
                    if (this.name)
                        autocompleteData[this.name] = this;
                });
            }
            else if (data)
                return { key: data.name, data: data };

            return autocompleteData;
        }, function (lecturer) {
            return {
                title: lecturer.name,
                value: lecturer.id
            };
        }, '.Id');

        function buildTimePeriodName(startTime, endTime) {
            var format = 'HH:mm';
            var start = moment.utc(startTime);
            var end = moment.utc(endTime);
            return start.format(format) + ' - ' + end.format(format);
        }

        function buildDayScheduleName(dayOfWeek, dedicatedDate) {
            if (!dedicatedDate)
                return dayOfWeek;

            var format = 'DD.MM';
            var date = moment.utc(dedicatedDate);
            return dayOfWeek + ' - ' + date.format(format);
        }

        Framework.Autocomplete.addTypeMapper('timeperiod', function (data) {
            var autocompleteData = {};
            if (Array.isArray(data)) {
                $.each(data, function () {
                    if (this.start && this.end)
                        autocompleteData[buildTimePeriodName(this.start, this.end)] = this;
                });
            }
            else if (data)
                return { key: buildTimePeriodName(data.start, data.end), data: data };

            return autocompleteData;
        }, function (timePeriod) {
            return {
                title: buildTimePeriodName(timePeriod.start, timePeriod.end),
                value: timePeriod.id
            };
        }, '.Id');

        Framework.Autocomplete.addTypeMapper('dayschedule', function (data) {
            var autocompleteData = {};
            if (Array.isArray(data)) {
                $.each(data, function () {
                    if (this.dayofweekstring)
                        autocompleteData[buildDayScheduleName(this.dayofweekstring, this.dedicateddate)] = this;
                });
            }
            else if (data)
                return { key: buildDayScheduleName(data.dayofweekstring, data.dedicateddate), data: data };

            return autocompleteData;
        }, function (daySchedule) {
            return {
                title: buildDayScheduleName(daySchedule.dayofweekstring, daySchedule.dedicateddate),
                value: daySchedule.id
            };
        }, '.Id');
    }
})();