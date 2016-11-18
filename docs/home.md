# Home(Schedule) page
___
## Activities descriptions
### Any user
* When document loaded or if user chooses another day(in datetime picker or via corresponding buttons),
ajax GET request to
[/Home/GetDaySchedule/{date}](#get_dayschedule)
is sent,
[DayScheduleViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Home/DayScheduleViewModel.cs)
 is received and
received events information is rendered to the schedule table, slider position is set.
* Slider movements are handled by JS/jQuery code (changes rooms colors on room map, horizontally scrolls
schedule table contents if needed and adds "Now" button if static "now" slider has gone out of view scope etc).
* When user hovers onto the audience at the room map, ajax GET request to
[/Audience/GetAudienceInfo/{audienceId}](#get_audienceinfo)
is sent and received
[AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)
is rendered.
* When user clicks on the audience at the room map, he gets redirected to /Audience/{audienceId}
* When user clicks on event at the schedule table, ajax GET request to
[/Event/DisplayEventPopup/{eventId}](#get_displayeventpopup)
is sent
and received popup is rendered on the page.
 * If user enters email and clicks join button, email is validated and then ajax POST request to
 /Event/Participate/{email}
 is sent. If respond indicated fail, fail reason is displayed in popup dialog.

### Logged in user
* If user clicks on free cell, popup for new event creation is shown.
 * ajax GET request to /Event/CreateNew is sent.
 * received partial view is rendered into popup.
___
## Requests
<a name="get_index">
* GET /Home/Index
</a>
 * Returns whole page with layout.
 * Razor engine uses passed [AvailableAudiencesViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AvailableAudiencesViewModel.cs)
 to render empty schedule table with headers and row names.
<a name="get_dayschedule">
* GET /Home/GetDaySchedule/{date}
</a>
 * Returns JSON (serialized [DayScheduleViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Home/DayScheduleViewModel.cs)).
<a name="get_audienceinfo">
* GET /Audience/GetAudienceInfo/{audienceId}
</a>
 * Returns JSON (serialized [AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)).
<a name="get_displayeventpopup">
* GET /Event/DisplayEventPopup
</a>
 * Returns PartialView, rendered using a [DisplayEventPopupViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventPopupViewModel.cs).
 * If user is logged in and is author of the event, edit and cancel buttons are rendered too.
