$(document)
    .ready(function() {
        $(".fa-plus")
            .click(function() {
                $("input[type='submit']").click();
            });
        var form = $("#join-event-form");
        var validator = form.data("validator");

        validator.settings.showErrors = function() {
            var visible = this.numberOfInvalids() === 0;
            $(".fa-plus").css("visibility", visible ? "visible" : "hidden");
            this.defaultShowErrors();
        };
    });