$(document).ready(function () {
    validation_icon = '<i class="fa fa-exclamation-circle" aria-hidden="true"></i> ';
    $(".prepend-exclamation-mark").each(function (i, val) {
        var $element = $(val);
        var innerHtml = $element.html();
        $element.html(validation_icon + innerHtml);
    });
});