var Framework = Framework || {};
(function () {
    if (Framework.Autocomplete) {
        Framework.Autocomplete.addTypeMapper('faculty', function (data) {
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
        }, function (faculty) {
            return {
                title: faculty.title,
                value: faculty.id
            };
        }, '.Id');

        Framework.Autocomplete.addTypeMapper('lecturer', function (data) {
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
        }, function (lecturer) {
            return {
                title: lecturer.title,
                value: lecturer.id
            };
        }, '.Id');
    }
})();