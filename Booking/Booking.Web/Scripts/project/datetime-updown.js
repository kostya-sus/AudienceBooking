function configureDatetimeUpdown(containerId, startDateId, endDateId) {
    var $container = $("#" + containerId);
    var $startDate = $("#" + startDateId);
    var $endDate = $("#" + endDateId);

    var $day = $container.find(".datetime-updown-day").first();
    var $month = $container.find(".datetime-updown-month").first();
    var $year = $container.find(".datetime-updown-year").first();
    var $startHour = $container.find(".datetime-updown-start-hour").first();
    var $startMinute = $container.find(".datetime-updown-start-minute").first();
    var $endHour = $container.find(".datetime-updown-end-hour").first();
    var $endMinute = $container.find(".datetime-updown-end-minute").first();

    var $dayView = $day.find("span").first();
    var $monthView = $month.find("span").first();
    var $yearView = $year.find("span").first();
    var $startHourView = $startHour.find("span").first();
    var $startMinuteView = $startMinute.find("span").first();
    var $endHourView = $endHour.find("span").first();
    var $endMinuteView = $endMinute.find("span").first();


    var startDate = new Date($startDate.val());
    var endDate = new Date($endDate.val());

    function setStartDate() {
        $startDate.val(startDate.toISOString()).trigger("change");
    }

    function setEndDate() {
        $endDate.val(endDate.toISOString()).trigger("change");
    }

    function updateView() {
        var day = startDate.getDate();
        day = (day >= 10 ? day : "0" + day) + ".";
        $dayView.text(day);
        var month = startDate.getMonth() + 1;
        month = (month >= 10 ? month : "0" + month) + ".";
        $monthView.text(month);
        $yearView.text(startDate.getFullYear().toString().substr(2, 2));

        var hours = startDate.getHours();
        hours = (hours >= 10 ? hours : "0" + hours) + ":";
        $startHourView.text(hours);
        var minutes = startDate.getMinutes();
        minutes = (minutes >= 10 ? minutes : "0" + minutes);
        $startMinuteView.text(minutes);

        hours = endDate.getHours();
        hours = (hours >= 10 ? hours : "0" + hours) + ":";
        $endHourView.text(hours);
        minutes = endDate.getMinutes();
        minutes = (minutes >= 10 ? minutes : "0" + minutes);
        $endMinuteView.text(minutes);
    }

    function isNewDateValid(startDate, endDate) {
        var now = new Date();
        var diff = Math.abs(endDate - startDate);
        var minutes = Math.floor(diff / 60000);
        return now < startDate && minutes >= 20;
    }

    function backupDateAndTryToChange(callback) {
        var prevStartDate = new Date(startDate.getTime());
        var prevEndDate = new Date(endDate.getTime());
        callback();
        if (isNewDateValid(startDate, endDate)) {
            setStartDate();
            setEndDate();
            updateView();
        } else {
            startDate = prevStartDate;
            endDate = prevEndDate;
        }
    }

    updateView();

    $day.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setDate(startDate.getDate() + 1);
                endDate.setDate(endDate.getDate() + 1);
            });
        });
    $day.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setDate(startDate.getDate() - 1);
                endDate.setDate(endDate.getDate() - 1);
            });
        });

    $month.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setMonth(startDate.getMonth() + 1);
                endDate.setMonth(endDate.getMonth() + 1);
            });
        });
    $month.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setMonth(startDate.getMonth() - 1);
                endDate.setMonth(endDate.getMonth() - 1);
            });
        });

    $year.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setYear(startDate.getYear() + 1);
                endDate.setYear(endDate.getYear() + 1);
            });
        });
    $year.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setYear(startDate.getYear() - 1);
                endDate.setYear(endDate.getYear() - 1);
            });
        });

    $startHour.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setHours(startDate.getHours() + 1);
            });
        });
    $startHour.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setHours(startDate.getHours() - 1);
            });
        });

    $endHour.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                endDate.setHours(endDate.getHours() + 1);
            });
        });
    $endHour.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                endDate.setHours(endDate.getHours() - 1);
            });
        });

    $startMinute.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setMinutes(startDate.getMinutes() + 1);
            });
        });
    $startMinute.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                startDate.setMinutes(startDate.getMinutes() - 1);
            });
        });

    $endMinute.find(".fa-caret-up")
        .click(function() {
            backupDateAndTryToChange(function() {
                endDate.setMinutes(endDate.getMinutes() + 1);
            });
        });
    $endMinute.find(".fa-caret-down")
        .click(function() {
            backupDateAndTryToChange(function() {
                endDate.setMinutes(endDate.getMinutes() - 1);
            });
        });
}