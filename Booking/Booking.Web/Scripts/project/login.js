$(document)
    .ready(function() {
        var form = $("form");
        var validator = form.data("validator");
        
        validator.settings.showErrors = function() {
            $('input[type="submit"]').prop('disabled', (this.numberOfInvalids() !== 0));
            this.defaultShowErrors();
        };
    });