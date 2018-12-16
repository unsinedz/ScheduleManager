var Framework = Framework || {};
Framework.ImplicitValues = (function () {
    var dataKey = 'implicitValue';

    function hasImplicitValue($element) {
        return $element.data(dataKey);
    }

    function setValueProvider($element, valueProvider) {
        if (typeof valueProvider === 'function')
            $element.data(dataKey, valueProvider);
    }

    function getValue($element) {
        if (hasImplicitValue($element))
            return $element.data(dataKey)();

        return $element.val();
    }

    function setDefaultProviders() {
        $(':input.timepicker').each(function () {
            var $element = $(this);
            setValueProvider($element, function () {
                return moment($element.val(), 'HH:mm').unix();
            })
        });
    }

    $(setDefaultProviders);

    return {
        getValue: getValue
    };
})();