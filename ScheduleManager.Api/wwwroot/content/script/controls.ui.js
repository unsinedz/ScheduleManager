(function ($) {
    var Align = (function () {
        var applyBlockCenterY = function (selector) {
            var $e = $(selector);
            if (!$e.length)
                return;

            var moveToCenter = function ($e) {
                var elementHeight = $e.height();
                var windowHeight = $(window).height();
                $e.css('position', 'relative');
                $e.animate({ 'top': getCenteredVerticalOffset(windowHeight, elementHeight) }, 150);
            }

            moveToCenter($e);
            $(window).resize(function () {
                moveToCenter($e);
            });
        };

        function getCenteredVerticalOffset(windowHeight, elementHeight) {
            return Math.max(0, windowHeight / 2.0 - elementHeight / 2.0);
        }

        return {
            applyBlockCenterY: applyBlockCenterY
        };
    })();

    Align.applyBlockCenterY('.vcenter-y');

    if (Framework && Framework.Navigation) {
        $(document).ready(function () {
            Framework.Navigation.init();
            $('.collapsible').collapsible();
            Framework.Navigation.unfoldActiveItem();
            var $fixedNav = $('.sidenav-fixed');
            if ($fixedNav.length)
                $('body .content').addClass('content-wrapper');
        });
    }
})(jQuery);