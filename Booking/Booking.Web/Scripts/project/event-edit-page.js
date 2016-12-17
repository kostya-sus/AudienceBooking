$(document)
    .ready(function() {
        configureDatetimeUpdown("event-date", "start-date", "end-date");

        $(".room-proxy").mouseenter(onRoomProxyMouseEnterChangeColor);
        $(".room-proxy").mouseleave(onRoomProxyMouseLeaveChangeColor);
        $(".room-proxy").click(onRoomProxyClickToggleChosenAudience);

        setChosenAudience();

        function checkIsFree() {
            var audienceId = $("#chosen-audience-id").val();
            var startDate = $("#start-date").val();
            var endDate = $("#end-date").val();
            var eventId = $("#event-id").val();

            var url = $("#audience-is-free-url").val() +
                "?audienceId=" +
                audienceId +
                "&startEvent=" +
                startDate +
                "&endEvent=" +
                endDate +
                "&eventId=" +
                eventId;


            $.get(url)
                .done(function(isFree) {
                    if (isFree.toLowerCase() === "true") {
                        $(".audience-not-free-message").css("visibility", "hidden");
                    } else {
                        $(".audience-not-free-message").css("visibility", "visible");
                    }
                });
        }

        checkIsFree();

        $("#start-date, #end-date, #chosen-audience-id").change(checkIsFree);

        $("#redirect-home-btn")
            .click(function() {
                var url = $("#redirect-to-home-url").val();
                window.location.replace(url);
            });

        $("#btn-cancel")
            .click(function() {
                var eventId = $("#event-id").val();
                var url = $("#display-event-url").val() + "?eventId=" + eventId;
                window.location.replace(url);
            });
    });