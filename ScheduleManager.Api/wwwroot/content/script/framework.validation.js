(function (jqVal) {
    if (jqVal) {
        jqVal.addMethod('autocompleterequired', function (value, element) {
            var $el = $(element);
            var value = $el.data('autocompleteValue');
            if (value === undefined)
                return false;

            return Object.keys(value).length;
        });
        jqVal.unobtrusive.adapters.add('autocompleterequired', [], function (options) {
            options.rules['autocompleterequired'] = {};
            if (options.message)
                options.messages['autocompleterequired'] = options.message;
        });
    }
})($.validator);