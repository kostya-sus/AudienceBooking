function displayCancelEventPopup() {
    var id = $("#btn-show-cancel-popup").attr("data-eventid");
    var eventUrl = $("#cancel-event-popup-url").val();
    eventUrl += "?eventId=" + id;
    $("#cancel-event-popup")
        .load(eventUrl,
            function() {
            });
}

function closeDisplayEventPopup() {
    var id = $("#Id").val();
    $("#display-event-popup-container-" + id).remove();
}

