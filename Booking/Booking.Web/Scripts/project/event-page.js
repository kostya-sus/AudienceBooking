$(document)
    .ready(function() {
        $(".fa-plus")
            .click(function() {
                $("#join-event-submit").click();
            });

        $(".fa-times")
            .click(function() {
                var submitId = $(this).data("form-id");
                $(submitId).click();
            });

        $("#redirect-audience-btn")
            .click(function() {
                var url = $("#redirect-to-audience-url").val();
                window.location.replace(url);
            });

        $("#redirect-home-btn")
            .click(function() {
                var url = $("#redirect-to-home-url").val();
                window.location.replace(url);
            });

        var form = $("#join-event-form");
        var validator = form.data("validator");

        validator.settings.showErrors = function() {
            var disabled = this.numberOfInvalids() !== 0;
            if (disabled) {
                $(".fa-plus").addClass("join-disabled");
            } else {
                $(".fa-plus").removeClass("join-disabled");
            }

            this.defaultShowErrors();
        };
    });