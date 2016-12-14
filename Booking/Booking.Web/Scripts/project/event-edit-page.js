$(document)
    .ready(function() {
        configureDatetimeUpdown("event-date", "start-date", "end-date");

        $(".room-proxy").mouseenter(onRoomProxyMouseEnterChangeColor);
        $(".room-proxy").mouseleave(onRoomProxyMouseLeaveChangeColor);
        $(".room-proxy").click(onRoomProxyClickToggleChosenAudience);

        setChosenAudience();
    });