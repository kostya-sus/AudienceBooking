function displayCancelEventPopup() {
    var id = $("#btn-show-cancel-popup").attr("data-eventid");
    var eventUrl = $("#cancel-event-popup-url").val();
    eventUrl += "?eventId=" + id;
    $("#cancel-event-popup")
        .load(eventUrl,
            function() {
            });
}
