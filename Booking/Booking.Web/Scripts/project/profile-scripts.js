    $(document).ready(function () {
        var el = $('#delete')[0];
        $("#change").click(function (e) {
            el.style.display = (el.style.display == 'none') ? 'block' : 'none'
        });
    });
