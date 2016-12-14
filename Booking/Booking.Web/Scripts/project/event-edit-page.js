$(document)
    .ready(function() {
        configureDatetimeUpdown("event-date", "start-date", "end-date");

        $(".room-proxy").mouseenter(onRoomProxyMouseEnterChangeColor);
        $(".room-proxy").mouseleave(onRoomProxyMouseLeaveChangeColor);
        $(".room-proxy").click(onRoomProxyClickToggleChosenAudience);

        setChosenAudience();

        function checkIsFree() {
            var audienceId = parseInt($("#chosen-audience-id").val());
            var startDateStr = $("#start-date").val();
            var startDate = new Date(startDateStr);
            var endDate = new Date($("#end-date").val());

            var diff = Math.abs(endDate - startDate);
            var duration = Math.floor(diff / 60000);

            var url = $("#audience-is-free-url").val() +
                "?audienceId=" +
                audienceId +
                "&dateTime=" +
                startDateStr +
                "&duration=" +
                duration;

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