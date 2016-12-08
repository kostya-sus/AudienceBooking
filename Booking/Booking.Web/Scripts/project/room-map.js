function onRoomProxyMouseEnter() {
    var targetDivId = $(this).data("room-target");
    $("#" + targetDivId).addClass("room-proxy-hover");
}

function onRoomProxyMouseLeave() {
    var targetDivId = $(this).data("room-target");
    $("#" + targetDivId).removeClass("room-proxy-hover");
}

$(document)
    .ready(function() {
        $(".room-proxy").mouseenter(onRoomProxyMouseEnter);
        $(".room-proxy").mouseleave(onRoomProxyMouseLeave);
    });