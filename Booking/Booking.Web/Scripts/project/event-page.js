$(document)
    .ready(function() {
        $(".fa-plus")
            .click(function() {
                $("input[type='submit']").click();
            });

        $(".email-input")
            .on("change textInput input",
                function() {
                    $("#join-event-form")
                        .validate({
                            rules: {
                                field: {
                                    required: true,
                                    email: true
                                }
                            },
                            submitHandler: function () {
                                alert("success");
                            },
                            invalidHandler:function() {
                                alert("error");
                            }
                        });
                });
    });