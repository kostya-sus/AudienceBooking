var availableAudiencesIds = [];
var availableAudiencesDivs;

function timestampIsBetween(time, left, right) {
    var timeMins = time.getHours() * 60 + time.getMinutes();
    var leftMins = left.getHours() * 60 + left.getMinutes();
    var rightMins = right.getHours() * 60 + right.getMinutes();
    return timeMins > leftMins && timeMins < rightMins;
}

function toggleActiveAudiences() {
    if (events === undefined || events === null) {
        return;
    }

    var time = posToTime(lowerHourBound, upperHourBound, tdWidth, $("#slider-draggable").position().left);

    var $div;

    for (var k = 0; k < availableAudiencesDivs.length; ++k) {
        $div = $(availableAudiencesDivs[k]);
        $div.removeClass("room-active");
    }

    for (var i = 0; i < events.length; i++) {
        var event = events[i];

        if (availableAudiencesIds.includes(event.AudienceId)) {
            var startTime = parseMvcDate(event.EventDateTime);
            var endTime = new Date(startTime.getTime() + event.Duration * 60000);
            if (timestampIsBetween(time, startTime, endTime)) {
                for (var j = 0; j < availableAudiencesDivs.length; ++j) {
                    $div = $(availableAudiencesDivs[j]);
                    if ($div.data("audience-id") === event.AudienceId) {
                        $div.addClass("room-active");
                        break;
                    }
                }
            }
        }
    }
}

$(document)
    .ready(function() {
        lowerHourBound = parseInt($("#LowerHourBound").val());
        upperHourBound = parseInt($("#UpperHourBound").val());
        tdWidth = parseInt($("#schedule-contents-table td").css("width"));
        tdHeight = parseInt($("#schedule-contents-table td").css("height"));
        thHeight = parseInt($("#schedule-contents-table th").css("height"));

        var rows = $(".audience-row");
        for (var i = 0; i < rows.length; ++i) {
            availableAudiencesIds.push($(rows[i]).data("audience-id"));
        }

        availableAudiencesDivs = $(".room-available");

        $("#slider-draggable")
            .draggable({
                axis: "x",
                containment: "parent",
                drag: function(event, ui) {
                    var t = posToTime(lowerHourBound, upperHourBound, tdWidth, ui.position.left);
                    setDraggableSliderCaption(timeToStringHHMM(t));
                    checkSliderNowPosition();
                    toggleActiveAudiences();
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

        var $datepicker = $("#datepicker");

        $datepicker.datepicker({ language: "ru" });

        $datepicker.datepicker("setDaysOfWeekDisabled", "06");

        $datepicker.on("changeDate",
            function() {
                dateChangedEvent($datepicker.datepicker("getDate"),
                    function() {
                        toggleActiveAudiences();
                    });
            });

        setDateToday();

        moveSliderNow(lowerHourBound, upperHourBound, tdWidth);

        toggleWithCalendarMode();

        $(".btn-goto-today").click(function() { setDateToday(toggleActiveAudiences); });

        $(".btn-goto-now")
            .click(function() {
                bindDraggableSliderToNow();
                toggleActiveAudiences();
            });

        checkSliderNowPosition();

        $(".room-proxy").mouseenter(onRoomProxyMouseEnterShowInfo);
        $(".room-proxy").mouseleave(onRoomProxyMouseLeaveHideInfo);

        setTimeout(toggleActiveAudiences, 300);
    });