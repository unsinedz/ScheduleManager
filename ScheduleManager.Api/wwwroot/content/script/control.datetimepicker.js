var Framework = Framework || {};
Framework.Datepicker = (function () {
    function initDate(selector) {
        selector = selector || '.datepicker';
        $(selector).datepicker();
    }

    function initTime(selector) {
        selector = selector || '.timepicker';
        $(selector).timepicker({
                twelveHour: false
            }
        );
    }

    $(function () {
        initDate();
        initTime();
    });

    return {
        initDate: initDate,
        initTime: initTime
    };
})();