# Home controller

## Requests

<a name="get_index">
* GET /Home/Index
</a>
 * Returns ViewResult.
 * Razor engine uses passed [HomeViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/HomeViewModel.cs)
 to render empty schedule table with headers and row names.
 * Divs that represent the rooms contain links to corresponding views.
 If the user is admin, all rooms have such links, otherwise - only available for booking rooms have it.
<a name="get_dayschedule">
* GET /Home/GetDaySchedule/{date}
</a>
 * Returns JSON (serialized [DayScheduleViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Home/DayScheduleViewModel.cs)).
<a name="get_audienceinfo">
 * Used to populate schedule table when [/Home/Index](#get_index) is loaded or chosen day is changed.
