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
    SetMonthName(value);
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
    SetMonthName(value);
    document.getElementById(id.toString()).value = value;
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