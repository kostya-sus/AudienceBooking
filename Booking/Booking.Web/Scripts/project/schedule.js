var lowerHourBound;
var upperHourBound;
var tdWidth;
var tdHeight;
var thHeight;
var events;

function posToTime(l, u, w, pos) {
    var time = new Date();
    var hours = Math.floor(l + pos / w);
    time.setHours(hours);
    var minutes = Math.floor((pos % w) * (60 / w));
    time.setMinutes(minutes);
    return time;
}

function timeToPos(l, u, w, time) {
    var hours = time.getHours();
    var minutes = time.getMinutes();
    return (hours - l) * w + (w / 60) * minutes;
}

function moveSlider(slider, pos) {
    slider.css("left", pos + "px");
}

function moveSliderNow(l, u, w) {
    var time = new Date();

    var currentHour = time.getHours();
    if (time.getMinutes() > 0) {
        currentHour++;
    }

    var isSliderNowInsideBounds = currentHour > lowerHourBound && currentHour <= upperHourBound;

    if (isSliderNowInsideBounds) {
        $("#slider-now").css("visibility", "visible");
    } else {
        $("#slider-now").css("visibility", "hidden");
        time.setHours(l);
        time.setMinutes(0);
    }

    var pos = timeToPos(l, u, w, time);
    moveSlider($("#slider-now"), pos);
    checkSliderNowPosition();
}

function timeToStringHHMM(time) {
    var hours = time.getHours();
    if (hours < 10) {
        hours = "0" + hours;
    }
    var minutes = time.getMinutes();
    if (minutes < 10) {
        minutes = "0" + minutes;
    }

    return hours + ":" + minutes;
}

function setDraggableSliderCaption(caption) {
    $("#slider-draggable .slider-caption").text(caption);
}

function toggleTableOnlyMode() {
    $("#schedule-mode-table-only").addClass("mode-button-selected");
    $("#schedule-mode-with-calendar").removeClass("mode-button-selected");
    $("#schedule-viewport-outer").width(870);
    $("#datepicker-container").hide();
}

function toggleWithCalendarMode() {
    $("#schedule-mode-with-calendar").addClass("mode-button-selected");
    $("#schedule-mode-table-only").removeClass("mode-button-selected");
    $("#schedule-viewport-outer").width(590);
    $("#datepicker-container").show();
    checkAndSetDraggableSliderPosition();
}

function checkAndSetDraggableSliderPosition(event, ui) {
    var pos = $("#slider-draggable").position().left;
    var viewportWidth = $("#schedule-viewport-outer").width();
    var currentScrollPos = $("#schedule-viewport-outer").scrollLeft();
    if (currentScrollPos > pos || currentScrollPos < pos + viewportWidth) {
        $("#schedule-viewport-outer").scrollLeft(pos - viewportWidth / 2);
    }
}

function dateChangedEvent(newDate, loadedCallback) {
    updateDayHeaderTitle(newDate);
    checkIfNewDateIsToday(newDate);
    checkSliderNowPosition();
    moveSliderNow(lowerHourBound, upperHourBound, tdWidth, new Date());

    loadSchedule(newDate, loadedCallback);
}

function refillSchedule(eventsList) {
    $(".schedule-event-item").remove();

    var $scheduleViewport = $("#schedule-viewport");

    eventsList.forEach(function(event) {
        var $div = $("<div></div>");
        var date = parseMvcDate(event.EventDateTime);

        var startTime = timeToStringHHMM(date);
        var endTime = timeToStringHHMM(new Date(date.getTime() + event.Duration * 60000));
        var $time = $("<span></span>");
        $time.addClass("event-item-time");

        if (!event.IsPublic) {
            $div.addClass("schedule-event-item-private");
        }

        if (event.Duration > 40) {
            $time.text(startTime + " - " + endTime);
            $time.addClass("event-item-time-big");
            var $title = $("<div></div>").text(event.Title);
            $title.addClass("event-item-title");
            $div.append($title);
        } else {
            $time.text(startTime + " - " + endTime);
            $div.addClass("event-item-small");
        }

        $div.append($time);

        $div.addClass("schedule-event-item");
        $div.css("left", timeToPos(lowerHourBound, upperHourBound, tdWidth, date) + 1);
        var ri = getRowIndex(event.AudienceId);
        $div.css("top", thHeight + ri * tdHeight + 2);
        $div.css("width", event.Duration * (tdWidth / 60.0) - 2);
        $div.css("height", tdHeight - 2);
        $.data($div, "event-id", event.Id);
        $scheduleViewport.append($div);
    });
}

function loadSchedule(date, loadedCallback) {
    var url = $("#get-day-schedule-url").val() + "?date=" + date.toLocaleDateString();
    $.getJSON(url)
        .done(function(data) {
            refillSchedule(data.Items);
            events = data.Items;
            loadedCallback();
        });
}

function getRowIndex(audienceId) {
    var $row = $("#audience-row-" + audienceId);
    return $row.data("row-index");
}

function parseMvcDate(dateStr) {
    return new Date(parseInt(dateStr.replace("/Date(", "").replace(")/", ""), 10));
}

function setDate(date) {
    var $datepicker = $("#datepicker");
    $datepicker.datepicker("update", date);
    dateChangedEvent(date);
}

function isWeekend(date) {
    return date.getDay() === 0 || date.getDay() === 6;
}

function incrementDate() {
    var $datepicker = $("#datepicker");
    var date = $datepicker.datepicker("getDate");
    date.setDate(date.getDate() + 1);
    setDate(date);
    if (isWeekend(date)) {
        incrementDate();
    }
}

function decrementDate() {
    var $datepicker = $("#datepicker");
    var date = $datepicker.datepicker("getDate");
    date.setDate(date.getDate() - 1);
    setDate(date);
    if (isWeekend(date)) {
        decrementDate();
    }
}

function checkIfNewDateIsToday(newDate) {
    var today = new Date();
    var isToday = today.getDate() === newDate.getDate() &&
        today.getMonth() === newDate.getMonth() &&
        today.getFullYear() === newDate.getFullYear();
    var $slider = $("#slider-now");
    if (isToday) {
        $slider.css("visibility", "visible");
        $(".btn-goto-today").css("visibility", "hidden");
    } else {
        $slider.css("visibility", "hidden");
        if (today > newDate) {
            $("#btn-goto-today-left").css("visibility", "hidden");
            $("#btn-goto-today-right").css("visibility", "visible");
        } else {
            $("#btn-goto-today-left").css("visibility", "visible");
            $("#btn-goto-today-right").css("visibility", "hidden");
        }
    }
}

function setDateToday() {
    var today = new Date();
    // TODO figure out, why just date = new Date() doesn`t work
    var date = new Date(today.getFullYear(), today.getMonth(), today.getDate());
    setDate(date);
}

function updateDayHeaderTitle(date) {
    var day = date.getDate();
    // TODO figure out, how to get localized strings from bootstrap-datepicker
    var daysMin = ["Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"];
    var dayOfWeek = daysMin[date.getDay()];
    var title = day + ", " + dayOfWeek;
    $("#day-header-title").text(title);
}

function checkSliderNowPosition() {
    var left = $("#schedule-viewport-outer").scrollLeft();
    var right = left + parseInt($("#schedule-viewport-outer").css("width"));
    var pos = $("#slider-now").position().left;
    var isInsideViewport = pos >= left && pos <= right;

    var today = new Date();
    var $datepicker = $("#datepicker");
    var date = $datepicker.datepicker("getDate");
    var isToday = today.getDate() === date.getDate() &&
        today.getMonth() === date.getMonth() &&
        today.getFullYear() === date.getFullYear();

    if (isInsideViewport || !isToday || $("#slider-now").css("visibility") === "hidden") {
        $(".btn-goto-now").css("visibility", "hidden");
    } else {
        if (pos < left) {
            $("#btn-goto-now-left").css("visibility", "visible");
            $("#btn-goto-now-right").css("visibility", "hidden");
        } else {
            $("#btn-goto-now-left").css("visibility", "hidden");
            $("#btn-goto-now-right").css("visibility", "visible");
        }
    }
}

function bindDraggableSliderToNow() {
    setDateToday();
    $("#slider-draggable").css("left", $("#slider-now").position().left);
    checkAndSetDraggableSliderPosition();
    checkSliderNowPosition();
    setDraggableSliderCaption(timeToStringHHMM(new Date()));
}