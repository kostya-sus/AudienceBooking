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



