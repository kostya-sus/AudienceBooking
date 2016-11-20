# Audience page

* Contains room map and schedule.
* Each time when user clicks audience on the room map, GET request to
[/Audience/{audienceId}](controllers/audience_controller.md#get_audience) is sent.
* After loading or when user changes dates by clicking corresponding buttons, ajax GET request to [/Audience/GetSchedule/{audienceId,startDate, endDate}](controllers/audience_controller.md#get_schedule) is sent and received information is displayed in schedule table
* When user hovers event on the schedule table, popups are displayed, similar to [Home page](home.md)
* If admin clicks close button, yes/no warning dialog is displayed. If admin clicks 'yes', ajax POST request to [/Audience/Close/{audienceId}](controllers/audience_controller.md#close_audience)
* If admin clicks open button at the closed audience page, ajax POST request to [/Audience/Open/{audienceId}](controllers/audience_controller.md#open_audience)
* If admin clicks edit button, ajax GET request to
[/Audience/Edit/{audienceId}](controllers/audience_controller.md#get_edit_form)
is sent. Popup with information about the audience is replaced with received edit form. If admin submits the form, ajax POST request to [/Audience/Save/{audienceInfoViewModel}](controllers/audience_controller.md#save_audience) is sent. Then ajax GET request to
[/Audience/GetAudienceInfo/{audienceId}](controllers/audience_controller.md#get_audienceinfo)
is sent and received display partial view replaces edit form partial view.
