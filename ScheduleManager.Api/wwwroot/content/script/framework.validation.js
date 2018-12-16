(function (jqVal) {
    function compare(val, other) {
        if (val == other)
            return 0;

        if (val > other)
            return -1;

        return 1;
    }

    function getValue($element) {
        return Framework.ImplicitValues.getValue($element);
    }

    if (jqVal) {
        jqVal.addMethod('autocompleterequired', function (value, element) {
            var $el = $(element);
            var autocompleteValue = $el.data('autocompleteValue');
            if (autocompleteValue === undefined)
                return false;

            return Object.keys(autocompleteValue).length;
        });

        jqVal.unobtrusive.adapters.add('autocompleterequired', [], function (options) {
            options.rules['autocompleterequired'] = {};
            if (options.message)
                options.messages['autocompleterequired'] = options.message;
        });

        jqVal.addMethod('signedcompare', function (value, element, parameters) {
            if (!parameters || parameters.to === undefined || parameters.sign === undefined)
                return true;
            
            var $element = $(element);
            var $otherElement = $element.parents('form').find(':input[name$="' + parameters.to + '"]');
            if (!$otherElement.length)
                return true;

            var comparison = compare(getValue($otherElement), getValue($element));
            if (parameters.sign === 0)
                return comparison === 0;

            return comparison * parameters.sign > 0;
        });

        jqVal.unobtrusive.adapters.add('signedcompare', ['to', 'sign'], function (options) {
            options.rules['signedcompare'] = {
                to: options.params.to,
                sign: options.params.sign
            };
            if (options.message)
                options.messages['signedcompare'] = options.message;
        });
    }
})($.validator);