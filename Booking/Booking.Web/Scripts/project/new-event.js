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

var ajaxSuccess = function() {
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
                            $("#new-event-popup .fa-caret-down, #new-event-popup .fa-caret-up")
                                .click(checkIfAudienceIsFree);

                            configureDatetimeUpdown("event-date", "start-date", "end-date");
                        });
            });
    });

function checkIfAudienceIsFree() {
    var audienceId = document.getElementById("ChosenAudienceId").value;

    var startDate = $("#start-date").val();
    var endDate = $("#end-date").val();

    var diff = Math.abs(endDate - startDate);
    var duration = Math.floor(diff / 60000);

    var url = $("#audience-is-free-url").val() +
        "?audienceId=" +
        audienceId +
        "&dateTime=" +
        startDate +
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