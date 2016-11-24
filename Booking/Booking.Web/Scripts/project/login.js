function prependExclamationMarkToValidationMessages() {
    var validationIcon = '<i class="fa fa-exclamation-circle" aria-hidden="true"></i> ';
    $(".prepend-exclamation-mark")
        .each(function(i, val) {
            var $element = $(val);
            if ($element.is(":empty")) {
                return;
            }

            var innerHtml = $element.html();
            $element.html(validationIcon + innerHtml);
        });
}

$(document)
    .ready(function() {
        var form = $("form");
        var validator = form.data("validator");

        validator.settings.showErrors = function() {
            $('input[type="submit"]').prop('disabled', (this.numberOfInvalids() === 0));
            this.defaultShowErrors();
        };
    });