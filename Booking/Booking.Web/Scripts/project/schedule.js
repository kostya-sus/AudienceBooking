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

function moveSliderNow(l, u, w) {
    var time = new Date();
    var pos = timeToPos(l, u, w, time);
    $("#slider-now").css("left", pos + "px");
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

$(document)
    .ready(function() {
        var lowerHourBound = parseInt($("#LowerHourBound").val());
        var upperHourBound = parseInt($("#UpperHourBound").val());
        var tdWidth = parseInt($("#schedule-contents-table td").css("width"));

        moveSliderNow(lowerHourBound, upperHourBound, tdWidth);

        setTimeout(function() {
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
    });