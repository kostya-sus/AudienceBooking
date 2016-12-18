var id = $("#Id").val();
$("#close-popup-" + id)
    .click(function() {
        $("#display-event-popup-container-"+id).remove();
    });

//$(document).ready(function() {
//    configureDatetimeUpdown("edit-event-popup-event-date", "new-event-popup-start-date", "new-event-popup-end-date");
//});
