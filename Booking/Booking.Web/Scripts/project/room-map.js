function onRoomProxyHover() {
    console.log($(this).data("room-target"));
}

$(document)
    .ready(function () {
        $(".room-proxy").hover(onRoomProxyHover);
    });