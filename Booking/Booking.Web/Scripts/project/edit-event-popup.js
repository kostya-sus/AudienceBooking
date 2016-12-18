function displayEventPopup() {
    var id = $("#Id").val();
    var url = "/Event/DisplayEventPopup?eventId=" + id;
    $("#display-event-popup-container-"+id).load(url);
        
    //$("#display-event-popup-container-" + id).remove();
}

//function saveChangesAndRedirect() {
//    var id = $("#Id").val();
//    var url = "/Event/DisplayEventPopup?eventId=" + id;
//    $("#display-event-popup-container-" + id).load(url);
//}

var onEventEditeSucceeded = function () {
    var id = $("#Id").val();
    var url = "/Event/DisplayEventPopup?eventId=" + id;
    $("#display-event-popup-container-" + id).load(url);
}

var onEventEditeFailured = function() {
    var id = $("#Id").val();
    var url = "/Event/Edit?eventId=" + id;
    $("#display-event-popup-container-" + id).remove();
    window.location.href = url;
}