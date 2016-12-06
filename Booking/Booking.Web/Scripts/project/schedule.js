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
    var pos = timeToPos(l, u, w, time);
    moveSlider($("#slider-now"), pos);
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
}

function setDate(date) {
    var $datepicker = $("#datepicker");
    $datepicker.datepicker("update", date);
    dateChangedEvent(date);
}

function incrementDate() {
    var $datepicker = $("#datepicker");
    var date = $datepicker.datepicker("getDate");
    date.setDate(date.getDate() + 1);
    setDate(date);
}

function decrementDate() {
    var $datepicker = $("#datepicker");
    var date = $datepicker.datepicker("getDate");
    date.setDate(date.getDate() - 1);
    setDate(date);
}

function checkIfNewDateIsToday(newDate) {
    var today = new Date();
    var isToday = today.getDate() === newDate.getDate() &&
        today.getMonth() === newDate.getMonth() &&
        today.getFullYear() === newDate.getFullYear();
    var $slider = $("#slider-now");
    if (isToday) {
        $slider.css("visibility", "visible");
    } else {
        $slider.css("visibility", "hidden");
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

$(document)
    .ready(function() {
        var lowerHourBound = parseInt($("#LowerHourBound").val());
        var upperHourBound = parseInt($("#UpperHourBound").val());
        var tdWidth = parseInt($("#schedule-contents-table td").css("width"));

        var time = new Date();
        var pos = timeToPos(lowerHourBound, upperHourBound, tdWidth, time);
        setDraggableSliderCaption(timeToStringHHMM(time));
        moveSlider($("#slider-now"), pos);
        moveSlider($("#slider-draggable"), pos);
        $("#schedule-viewport-outer").scrollLeft(pos);

        setInterval(function() {
                moveSliderNow(lowerHourBound, upperHourBound, tdWidth);
            },
            60000);

        $("#slider-draggable")
            .draggable({
                axis: "x",
                containment: "parent",
                drag: function(event, ui) {
                    var time = posToTime(lowerHourBound, upperHourBound, tdWidth, ui.position.left);
                    setDraggableSliderCaption(timeToStringHHMM(time));
                },
                stop: checkAndSetDraggableSliderPosition
            });


        $("#schedule-mode-table-only").click(toggleTableOnlyMode);
        $("#schedule-mode-with-calendar").click(toggleWithCalendarMode);

        $("#decrement-date-button").click(decrementDate);
        $("#increment-date-button").click(incrementDate);

        configureDatepicker();

        setDateToday();

        toggleWithCalendarMode();
    });