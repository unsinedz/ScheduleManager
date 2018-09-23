var Navigation = (function ($) {
    function activateMatchingNavigation(selector, isActiveFunction) {
        if (!selector)
            return;

        $(selector).filter(isActiveFunction).toggleClass('active', true);
    }

    function openNavigationWithActiveItem($nav, groupSelector, $item) {
        var $itemGroup = $item.closest(groupSelector);
        M.Collapsible.getInstance($nav).open($nav.find(groupSelector).index($itemGroup));
    }

    var $mainnav = $('.mainnav');
    var init = function () {
        var preparedUrl = window.location.pathname.trim('/').toLowerCase();
        activateMatchingNavigation('.mainnav ul li', function () {
            var href = $(this).find('a').attr('href');
            if (!href)
                return false;

            var paramStartIndex = href.indexOf('?');
            if (paramStartIndex < 0)
                paramStartIndex = href.length;

            var preparedHref = href.substring(0, paramStartIndex).trim('/').toLowerCase();
            return preparedUrl === preparedHref;
        });
    };

    return {
        init: init,
        unfoldActiveItem: function () {
            openNavigationWithActiveItem($mainnav, '.item', $mainnav.find('ul .active'));
        }
    };
})(jQuery);