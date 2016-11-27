﻿function posToTime(l, u, w, pos) {
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
}

function toggleWithCalendarMode() {
    $("#schedule-mode-with-calendar").addClass("mode-button-selected");
    $("#schedule-mode-table-only").removeClass("mode-button-selected");
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
                }
            });


        $("#schedule-mode-table-only").click(toggleTableOnlyMode);
        $("#schedule-mode-with-calendar").click(toggleWithCalendarMode);
        toggleTableOnlyMode();
    });