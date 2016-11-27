$(document)
    .ready(function() {
        $("#slider-draggable")
            .draggable({
                axis: "x",
                containment: "parent",
                drag:function(event, ui) {
                    console.log(ui.position.left);
                }
            });
    });