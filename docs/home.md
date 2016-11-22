# Home(Schedule) page
___
## Activities descriptions
### Any user
* When document loaded or if user chooses another day(in datetime picker or via corresponding buttons),
ajax GET request to
[/Home/GetDaySchedule/{date}](controllers/home_controller.md#get_dayschedule)
is sent,
[DayScheduleViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Home/DayScheduleViewModel.cs)
 is received and
received events information is rendered to the schedule table, slider position is set.
 * divs, that represent audiences on the room map, contain links to corresponding pages.
 * if the user is admin, all the rooms contain links, otherwise only available for booking rooms contain links.
* Slider movements are handled by JS/jQuery code (changes rooms colors on room map, horizontally scrolls
schedule table contents if needed and adds "Now" button if static "now" slider has gone out of view scope etc).
* When user hovers onto the audience at the room map, ajax GET request to
[/Audience/GetAudienceInfo/{audienceId}](controllers/audience_controller.md#get_audienceinfo)
is sent and received
[AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)
is rendered.
* When user clicks on the audience at the room map, he gets redirected to
[/Audience/{audienceId}](controllers/audience_controller.md#get_audience)
* When user clicks on event at the schedule table, ajax GET request to
[/Event/DisplayEventPopup/{eventId}](controllers/event_controller.md#get_displayeventpopup)
is sent and if user can view information about this event, popup is received and rendered on the page.
 * If user clicks chain button, he gets redirected to
 [/Event/{eventId}](controllers/event_controller.md#get_event)
 * If user enters email and clicks join button, email is validated and then ajax POST request to
 [/Event/Participate/{email}](controllers/event_controller.md#participate)
 is sent. If respond indicated fail, fail reason is displayed in popup dialog. Otherwise
 success message is displayed.

### Logged in user
* If user clicks on free cell, popup for new event creation is shown.
 * ajax GET request to [/Event/Create/{audienceId, eventDateTime, duration}](controllers/event_controller.md#get_create_audience_id) is sent.
 * received partial view is rendered into popup.
 * when the user clicks create button, ajax POST request to
 [/Event/Create/{createEditEventViewModel}](controllers/event_controller.md#create) is sent after
 client-side validation.
* If user is author of the opened in DisplayEventPopup event or admin, he can edit or cancel the event.
 * When user changes audience or booking period, ajax GET request is sent to
 [/Audience/IsFree/{audienceId, dateTime, duration}](controllers/audience_controller.md#is_free). If respond contains false, corresponding message is displayed.
 * If user clicks cancel button, DisplayEventPopup is hidden and yes/no dialog is shown. If user confirms cancelation, ajax DELETE request to
 [/Event/Cancel/{eventId}](controllers/event_controller.md#cancel) is sent,
 both popups are hidden, event removed from schedule. Otherwise yes/no dialog is hidden, DisplayEventPopup is shown.
 * If user clicks edit button, ajax GET request to
 [/Event/EditPopup/{createEditEventViewModel}](controllers/event_controller.md#edit_popup) is sent, received partial view is displayed in popup. When user clicks save button, ajax POST request to
 [/Event/Save/{createEditEventViewModel}](controllers/event_controller.md#save) is sent after client-side validation.
