var id = $("#Id").val();
$("#close-popup-" + id)
    .click(function() {
        $("#display-event-popup-container-"+id).remove();
    });
