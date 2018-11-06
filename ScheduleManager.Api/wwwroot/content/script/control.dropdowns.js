var Framework = Framework || {};
Framework.Dropdowns = (function () {
    function init(selector) {
        $(selector).formSelect();
    }
     
    $(function () {
        init('select');
    });

    return {
        init: init
    };
})();