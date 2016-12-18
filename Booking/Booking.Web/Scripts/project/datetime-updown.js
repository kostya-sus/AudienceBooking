function configureDatetimeUpdown(containerId, startDateId, endDateId) {
    var scheduleRuleUrl = $("#get-schedule-rule-url").val();
    var nextAvailableDateUrl = $("#get-next-previous-date-url").val();

    function getScheduleRuleUrl(date) {
        return scheduleRuleUrl + "?date=" + date.toISOString();
    }

    var currentScheduleRule;

    function updateScheduleRule(date) {
        $.get(getScheduleRuleUrl(date))
            .done(function(data) {
                currentScheduleRule = data;
            });
    }

    var $container = $("#" + containerId);
    var $startDate = $("#" + startDateId);
    var $endDate = $("#" + endDateId);

    var $day = $container.find(".datetime-updown-day").first();
    var $month = $container.find(".datetime-updown-month").first();
    var $year = $container.find(".datetime-updown-year").first();
    var $startHour = $container.find(".datetime-updown-start-hour").first();
    var $startMinute = $container.find(".datetime-updown-start-minute").first();
    var $endHour = $container.find(".datetime-updown-end-hour").first();
    var $endMinute = $container.find(".datetime-updown-end-minute").first();

    var $dayView = $day.find("span").first();
    var $monthView = $month.find("span").first();
    var $yearView = $year.find("span").first();
    var $startHourView = $startHour.find("span").first();
    var $startMinuteView = $startMinute.find("span").first();
    var $endHourView = $endHour.find("span").first();
    var $endMinuteView = $endMinute.find("span").first();

    var monthDisplayedValues;
    var displayMonthName = $month.hasClass("datetime-updown-month-name");
    if (displayMonthName) {
        monthDisplayedValues = [
            "Января", "Февраля", "Марта", "Апреля",
            "Мая", "Июня", "Июля", "Августа", "Сентября",
            "Октября", "Ноября", "Декабря"
        ];
    } else {
        monthDisplayedValues = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
    }

    var startDate = new Date($startDate.val());
    var endDate = new Date($endDate.val());


    updateScheduleRule(startDate);

    function setStartDate() {
        $startDate.val(startDate.toISOString()).trigger("change");
    }

    function setEndDate() {
        $endDate.val(endDate.toISOString()).trigger("change");
    }

    function updateView() {
        var day = startDate.getDate();
        day = (day >= 10 ? day : "0" + day) + (displayMonthName ? "" : ".");
        $dayView.text(day);
        var month = startDate.getMonth();
        month = monthDisplayedValues[month];
        $monthView.text(month);
        $yearView.text(startDate.getFullYear().toString().substr(2, 2));

        var hours = startDate.getHours();
        hours = (hours >= 10 ? hours : "0" + hours) + ":";
        $startHourView.text(hours);
        var minutes = startDate.getMinutes();
        minutes = (minutes >= 10 ? minutes : "0" + minutes);
        $startMinuteView.text(minutes);

        hours = endDate.getHours();
        hours = (hours >= 10 ? hours : "0" + hours) + ":";
        $endHourView.text(hours);
        minutes = endDate.getMinutes();
        minutes = (minutes >= 10 ? minutes : "0" + minutes);
        $endMinuteView.text(minutes);
    }

    function isDateValid(eventStart, eventEnd) {
        var startHours = eventStart.getHours();
        var endHours = eventEnd.getHours();
        if (eventEnd.getMinutes()) {
            endHours ++;
        }
        if (startHours < lowerHoursBound || startHours > upperHoursBound) {
            return false;
        }
        if (endHours > upperHoursBound || endHours < lowerHoursBound) {
            return false;
        }

        var dateNow = new Date();
        var diff = eventEnd - eventStart;
        var minutes = Math.floor(diff / 60000);
        return dateNow < eventStart && minutes >= 20;
    }

    function addMinutes(date, minutes) {
        return new Date(date.getTime() + minutes * 60000);
    }

    function diffInMinutes(date1, date2) {
        return Math.floor((date2 - date1) / 60000);
    }

    function backupDateAndTryToChange(callback) {
        var prevStartDate = new Date(startDate.getTime());
        var prevEndDate = new Date(endDate.getTime());
        callback();
        var diff = endDate - startDate;
        var minutes = Math.floor(diff / 60000);
        if (minutes < 20) {
            endDate.setMinutes(startDate.getMinutes() + 20);
        }

        setStartDate();
        setEndDate();
        updateView();
    }

    function setAndUpdate() {
        setStartDate();
        setEndDate();
        updateView();
    }

    if (!isDateValid(startDate, endDate)) {
        var now = new Date();
        startDate.setDate(now.getDate() + 1);
        startDate.setHours(lowerHoursBound);
        startDate.setMinutes(0);

        endDate = new Date(startDate.getTime());
        endDate.setMinutes(endDate.getMinutes() + 30);
    }

    startDate.setSeconds(0);
    startDate.setMilliseconds(0);
    endDate.setSeconds(0);
    endDate.setMilliseconds(0);

    setStartDate();
    setEndDate();

    updateView();

    $day.find(".fa-caret-up")
        .click(function() {
            startDate.setDate(startDate.getDate() + 1);
            endDate.setDate(endDate.getDate() + 1);
        });
    $day.find(".fa-caret-down")
        .click(function() {
            startDate.setDate(startDate.getDate() - 1);
            endDate.setDate(endDate.getDate() - 1);
        });

    $month.find(".fa-caret-up")
        .click(function() {
            startDate.setMonth(startDate.getMonth() + 1);
            endDate.setMonth(endDate.getMonth() + 1);
        });
    $month.find(".fa-caret-down")
        .click(function() {
            startDate.setMonth(startDate.getMonth() - 1);
            endDate.setMonth(endDate.getMonth() - 1);
        });

    $year.find(".fa-caret-up")
        .click(function() {
            startDate.setYear(startDate.getYear() + 1);
            endDate.setYear(endDate.getYear() + 1);
        });
    $year.find(".fa-caret-down")
        .click(function() {
            startDate.setYear(startDate.getYear() - 1);
            endDate.setYear(endDate.getYear() - 1);
        });

    $startHour.find(".fa-caret-up")
        .click(function() {
            startDate.setHours(startDate.getHours() + 1);
        });
    $startHour.find(".fa-caret-down")
        .click(function() {
            startDate.setHours(startDate.getHours() - 1);
        });

    $endHour.find(".fa-caret-up")
        .click(function() {
            endDate.setHours(endDate.getHours() + 1);
        });
    $endHour.find(".fa-caret-down")
        .click(function() {
            endDate.setHours(endDate.getHours() - 1);
        });

    $startMinute.find(".fa-caret-up")
        .click(function() {
            startDate.setMinutes(startDate.getMinutes() + 10);
        });
    $startMinute.find(".fa-caret-down")
        .click(function() {
            startDate.setMinutes(startDate.getMinutes() - 10);
        });

    $endMinute.find(".fa-caret-up")
        .click(function() {
            endDate.setMinutes(endDate.getMinutes() + 10);
        });
    $endMinute.find(".fa-caret-down")
        .click(function() {
            endDate.setMinutes(endDate.getMinutes() - 10);
        });
}