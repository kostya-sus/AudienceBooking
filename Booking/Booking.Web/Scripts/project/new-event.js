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

function incrementMinuteValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 30 : value;
    if (value !== 55) {
        value+=5;
    }
    document.getElementById(id.toString()).value = value;
}

function decrementMinuteValue(id) {
    var value = parseInt(document.getElementById(id.toString()).value);
    value = isNaN(value) ? 30 : value;
    if (value !== 0) {
        value-=5;
    }
    document.getElementById(id.toString()).value = value;
}