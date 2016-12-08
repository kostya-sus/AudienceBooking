function hasTickedBox() {
    var checkBox = document.getElementById('checkboxRed');
    if(checkBox.checked)
    {
        document.getElementById('private').style.color = "#f95752";
        document.getElementById('public').style.color = "#afb2bb";
    }
    else
    {
        document.getElementById('public').style.color = "#f95752";
        document.getElementById('private').style.color = "#afb2bb";
    }
};

$(document).ready(function () {
    $("#btn-new-event-popup").click(function () {
        var eventUrl = $("#get-new-event-popup-url").val();
        $("#new-event-popup").load(eventUrl,
            function () {
            });
    });
});


function incrementDayValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 31) {
        value++;
    }
    document.getElementById(id.toString()).value = value;
}

function decrementDayValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 1) {
        value--;
    }
    document.getElementById(id.toString()).value = value;
}

function incrementHourValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 12 : value;
    if (value !== 19) {
        value++;
    }
    document.getElementById(id.toString()).value = value;
}

function decrementHourValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 12 : value;
    if (value !== 12) {
        value--;
    }
    document.getElementById(id.toString()).value = value;
}

function incrementMinuteValue(idMinute, idHour) {
    var value = parseInt(document.getElementById(idMinute.toString()).value);
    value = isNaN(value) ? 30 : value;
    if (value === 55) {
        value = 0;
        incrementDayValue(idHour);
    } else {
        value += 5;
    }
    document.getElementById(idMinute.toString()).value = value;
}

function decrementMinuteValue(idMinute, idHour) {
    var value = parseInt(document.getElementById(idMinute.toString()).value);
    value = isNaN(value) ? 30 : value;
    if (value === 0) {
        value = 55;
        decrementHourValue(idHour);
    } else {
        value-=5;
    }
    document.getElementById(idMinute.toString()).value = value;
}

function NextMonth(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 12) {
        value += 1;
    } else {
        value = 1;
    }
    document.getElementById(id.toString()).value = value;
}

function PrevMonth(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 1 : value;
    if (value !== 1) {
        value -= 1;
    } else {
        value = 12;
    }
    document.getElementById(id.toString()).value = value;
}