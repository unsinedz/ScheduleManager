var Framework = Framework || {};
Framework.Utils = (function () {
    return {
        getAntiForgeryToken: function ($form) {
            var selector = 'input[name="__RequestVerificationToken"]';
            if ($form && $form.length)
                return $(selector, $form).val();

            return $(selector).val();
        }
    };
})();