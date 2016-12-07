var lowerHourBound;
var upperHourBound;
var tdWidth;

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

function configureDatepicker() {
    var $datepicker = $("#datepicker");

    $datepicker.datepicker({ language: "ru" });

    $datepicker.datepicker("setDaysOfWeekDisabled", "06");

    $datepicker.on("changeDate",
        function() {
            dateChangedEvent($datepicker.datepicker("getDate"));
        });
}

function checkAndSetDraggableSliderPosition(event, ui) {
    var pos = $("#slider-draggable").position().left;
    var viewportWidth = $("#schedule-viewport-outer").width();
    var currentScrollPos = $("#schedule-viewport-outer").scrollLeft();
    if (currentScrollPos > pos || currentScrollPos < pos + viewportWidth) {
        $("#schedule-viewport-outer").scrollLeft(pos - viewportWidth / 2);
    }
}

function dateChangedEvent(newDate) {
    updateDayHeaderTitle(newDate);
    checkIfNewDateIsToday(newDate);
    checkSliderNowPosition();
    moveSliderNow(lowerHourBound, upperHourBound, tdWidth, new Date());

    loadSchedule(newDate);
}

function loadSchedule(date) {
    
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

$(document)
    .ready(function() {
        lowerHourBound = parseInt($("#LowerHourBound").val());
        upperHourBound = parseInt($("#UpperHourBound").val());
        tdWidth = parseInt($("#schedule-contents-table td").css("width"));


        $("#slider-draggable")
            .draggable({
                axis: "x",
                containment: "parent",
                drag: function(event, ui) {
                    var t = posToTime(lowerHourBound, upperHourBound, tdWidth, ui.position.left);
                    setDraggableSliderCaption(timeToStringHHMM(t));
                    checkSliderNowPosition();
                },
                stop: checkAndSetDraggableSliderPosition
            });

        var time = new Date();
        var pos = timeToPos(lowerHourBound, upperHourBound, tdWidth, time);

        if (pos > (upperHourBound - lowerHourBound) * tdWidth) {
            pos = (upperHourBound - lowerHourBound) * tdWidth;
        } else if (pos < 0) {
            pos = 0;
        }

        moveSlider($("#slider-now"), pos);
        moveSlider($("#slider-draggable"), pos);
        $("#schedule-viewport-outer").scrollLeft(pos);

        setDraggableSliderCaption(timeToStringHHMM(time));

        setInterval(function() {
                moveSliderNow(lowerHourBound, upperHourBound, tdWidth);
            },
            60000);


        $("#schedule-mode-table-only").click(toggleTableOnlyMode);
        $("#schedule-mode-with-calendar").click(toggleWithCalendarMode);

        $("#decrement-date-button").click(decrementDate);
        $("#increment-date-button").click(incrementDate);

        configureDatepicker();

        setDateToday();

        moveSliderNow(lowerHourBound, upperHourBound, tdWidth);

        toggleWithCalendarMode();

        $(".btn-goto-today").click(setDateToday);

        $(".btn-goto-now").click(bindDraggableSliderToNow);

        checkSliderNowPosition();
    });