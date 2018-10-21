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

        Framework.Autocomplete.addTypeMapper('department', function (data) {
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
        }, function (department) {
            return {
                title: department.title,
                value: department.id
            };
        }, '.Id');
    }
})();