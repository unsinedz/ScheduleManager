(function ($) {
    function processPostUrls(selector) {
        if (!selector)
            return;

        $(document).on('click', selector, function (e) {
            e.preventDefault();
            $.post($(this).attr('data-post-url'), {
                "__RequestVerificationToken": Framework.Utils.getAntiForgeryToken()
            }).done(function () {
                window.location.reload(true);
            });
        });
    }

    processPostUrls('[data-post-url]');
})(jQuery);