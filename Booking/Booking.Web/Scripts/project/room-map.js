function onRoomProxyMouseEnterShowInfo() {
    var targetDivId = "#" + $(this).data("room-target");
    $(targetDivId).addClass("room-proxy-hover");
    var audienceId = $(targetDivId).data("audience-id");
    var audienceInfoUrl = $("#get-audience-info-url").val();
    var $div = $("<div class='audience-info'></div>");
    $("#room-map-container").append($div);
    $div.load(audienceInfoUrl + "?audienceId=" + audienceId);
}

function onRoomProxyMouseLeaveHideInfo() {
    var targetDivId = $(this).data("room-target");
    $("#" + targetDivId).removeClass("room-proxy-hover");
    $(".audience-info").remove();
}

function onRoomProxyMouseEnterChangeColor() {
    var $targetDiv = $("#" + $(this).data("room-target"));
    $targetDiv.addClass("room-proxy-hover");
}

function onRoomProxyMouseLeaveChangeColor() {
    var $targetDiv = $("#" + $(this).data("room-target"));
    $targetDiv.removeClass("room-proxy-hover");
}

function onRoomProxyClickToggleChosenAudience() {
    $(".room-available").removeClass("room-active");
    var $targetDiv = $("#" + $(this).data("room-target"));
    $targetDiv.addClass("room-active");
    $("#chosen-audience-id").val($targetDiv.data("audience-id")).trigger("change");
    setChosenAudience();
}

function setChosenAudience() {
    var id = $("#chosen-audience-id").val();
    $(".room-available")
        .each(function() {
            var $room = $(this);
            if (parseInt($room.data("audience-id")) === id) {
                $room.addClass("room-active");
            }
        });
    var audienceName = $("#audience-" + id + "-name").val();
    $("#audience-name").text(audienceName);
}

function onRoomProxyClickRedirectToAudiencePage() {
    var targetDivId = "#" + $(this).data("room-target");
    $(targetDivId).addClass("room-proxy-hover");
    var audienceId = $(targetDivId).data("audience-id");
    var url = $("#audience-page-url").val() + "?audienceId=" + audienceId;
    window.location.replace(url);
}