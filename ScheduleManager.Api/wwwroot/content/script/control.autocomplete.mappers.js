var Framework = Framework || {};
(function () {
    if (Framework.Autocomplete) {
        Framework.Autocomplete.addTypeMapper('faculty', function (data) {
            var autocompleteData = {};
            if (data && data.length) {
                $.each(data, function () {
                    if (this.title)
                        autocompleteData[this.title] = this;
                });
            }

            return autocompleteData;
        }, function (faculty) {
            return {
                title: faculty.title,
                value: faculty.id
            };
        }, '.Id');

        Framework.Autocomplete.addTypeMapper('lecturer', function (data) {
            var autocompleteData = {};
            if (data && data.length) {
                $.each(data, function () {
                    if (this.title)
                        autocompleteData[this.title] = this;
                });
            }

            return autocompleteData;
        }, function (faculty) {
            return {
                title: faculty.title,
                value: faculty.id
            };
        }, '.Id');
    }
})();