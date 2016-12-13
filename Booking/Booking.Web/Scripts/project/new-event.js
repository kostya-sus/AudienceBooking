function hasTickedBox() {
    var checkBox = document.getElementById('checkboxRed');
    if (checkBox.checked) {
        document.getElementById('private').style.color = "#f95752";
        document.getElementById('public').style.color = "#afb2bb";
    } else {
        document.getElementById('public').style.color = "#f95752";
        document.getElementById('private').style.color = "#afb2bb";
    }
};

var ajaxSuccess = function () {
    $("#myModal").modal('hide');
}
$(document)
    .ready(function() {
        $("#btn-new-event-popup")
            .click(function() {
                var eventUrl = $("#get-new-event-popup-url").val();
                $("#new-event-popup")
                    .load(eventUrl,
                        function() {
                            for (var i = 0; i < 12; i++) {
                                incrementHourValue('StartHour', 'StartMinute','labelStartHour', 'labelStartMinute');
                                incrementHourValue('EndHour', 'EndMinute', 'labelEndHour','labelEndMinute');
                            }
                            incrementDayValue('EventDay', 'labelDay');
                            incrementMinuteValue('StartMinute', 'StartHour', 'labelStartMinute','labelStartHour');
                            incrementMinuteValue('EndMinute', 'EndHour', 'labelEndMinute', 'labelEndHour');
                            NextMonth('EventMonth');

                            $("#new-event-popup .fa-caret-down, #new-event-popup .fa-caret-up")
                                .click(checkIfAudienceIsFree);
                        });
                $(".btnCreate")
                    .click(function () {

                    });
            });
    });

function checkIfAudienceIsFree() {
    var audienceId = document.getElementById("ChosenAudience").value;

    var day = parseInt(document.getElementById('EventDay').value);
    var month = parseInt(document.getElementById('EventMonth').value );
    var hourFrom = parseInt(document.getElementById('StartHour').value);
    var minuteFrom = parseInt(document.getElementById('StartMinute').value);
    var hourTo = parseInt(document.getElementById('EndHour').value);
    var minuteTo = parseInt(document.getElementById('EndMinute').value);

    var dateNow = new Date();
    var year = dateNow.getFullYear();

    var time = month + "/" + day + "/" + year + " " + hourFrom + ':' + minuteFrom + ':' + 0;

    var duration = (hourTo - hourFrom) * 60 + (minuteTo - minuteFrom);

    var url = $("#audience-is-free-url").val() +
        "?audienceId=" +
        audienceId +
        "&dateTime=" +
        time +
        "&duration=" +
        duration;

    $.get(url)
        .done(function(isFree) {
            toggleIsFreeMessage(isFree);
        });
}

function toggleIsFreeMessage(isFree) {
    if (isFree === 'False') {
        $("#errorMessage").css("visibility", 'visible');
    } else {
        $("#errorMessage").css("visibility", 'hidden');
    }
    
}

function changeValue(id, value) {
    $("#" + id).attr("value", value);
}

function incrementDayValue(id, labelId) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 31) {
        value++;
    }
    changeValue(id, value);
    changeValue(labelId, value);
}

function decrementDayValue(id, labelId) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 1) {
        value--;
    }
    changeValue(id, value);
    changeValue(labelId, value);
}

function incrementHourValue(id, minuteId, labelId, minuteLabelId) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 12 : value;
    if (value !== 19) {
        value++;
        if (value === 19) {
            value = 0;
            changeValue(minuteId, value);
            changeValue(minuteLabelId, value);
            //document.getElementById(minuteId.toString()).value = 0;
        }
    }
    changeValue(id, value);
    changeValue(labelId, value);
}

function decrementHourValue(id, lableId) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 12 : value;
    if (value !== 12) {
        value--;
    }
    changeValue(id, value);
    changeValue(lableId, value);
}

function incrementMinuteValue(idMinute, idHour, lableMinuteId, lableHourId) {
    var value = parseInt(document.getElementById(idMinute.toString()).value);
    value = isNaN(value) ? 30 : value;
    if (value === 55) {
        value = 0;
        incrementHourValue(idHour, idMinute, lableHourId, lableMinuteId);
    } else {
        value += 5;
    }
    changeValue(idMinute, value);
    changeValue(lableMinuteId, value);
}

function decrementMinuteValue(idMinute, idHour, labelMinuteId, labelHourId) {
    var value = parseInt(document.getElementById(idMinute).value);
    value = isNaN(value) ? 30 : value;
    if (value === 0) {
        value = 55;
        decrementHourValue(idHour, labelHourId);
    } else {
        value -= 5;
    }
    changeValue(idMinute, value);
    changeValue(labelMinuteId, value);
}

function NextMonth(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 12) {
        value += 1;
    } else {
        value = 1;
    }
    SetMonthName(value);
    changeValue(id, value);
}

function PrevMonth(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 1) {
        value -= 1;
    } else {
        value = 12;
    }
    SetMonthName(value);
    changeValue(id, value);
}

function SetMonthName(value) {
    if (value === 1) {
        document.getElementById('monthLabel').value = 'Января';
    }
    if (value === 2) {
        document.getElementById('monthLabel').value = 'Февраля';
    }
    if (value === 3) {
        document.getElementById('monthLabel').value = ' Марта';
    }
    if (value === 4) {
        document.getElementById('monthLabel').value = 'Апреля';
    }
    if (value === 5) {
        document.getElementById('monthLabel').value = '  Мая';
    }
    if (value === 6) {
        document.getElementById('monthLabel').value = ' Июня';
    }
    if (value === 7) {
        document.getElementById('monthLabel').value = ' Июля';
    }
    if (value === 8) {
        document.getElementById('monthLabel').value = 'Августа';
    }
    if (value === 9) {
        document.getElementById('monthLabel').value = 'Сентября';
    }
    if (value === 10) {
        document.getElementById('monthLabel').value = 'Октября';
    }
    if (value === 11) {
        document.getElementById('monthLabel').value = 'Ноября';
    }
    if (value === 12) {
        document.getElementById('monthLabel').value = 'Декабря';
    }
}

