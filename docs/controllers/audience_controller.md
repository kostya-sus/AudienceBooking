# Audience controller

## Requests
<a name="get_audienceinfo">
* GET /Audience/GetAudienceInfo/{audienceId}
</a>
 * Returns JSON (serialized [AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)).

<a name="get_audience">
* GET /Audience/{audienceId}
</a>
 * Checks if user can view specified audience information (only available for booking audiences for normal users and all audiences for admin)
 * Returns View, rendered using  [AudienceViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceViewModel.cs).
 * If the user is admin, edit and open/close buttons are available. Also links to closed audiences are added to the room map.
 * Route and audience info popup are shown on the room map.

<a name="get_schedule">
* GET /Audience/GetSchedule/{audienceId,startDate, endDate}
</a>
 * Returns all events that will take part at the specified audience between specified dates.

<a name="is_free">
* GET /Audience/IsFree/{audienceId, dateTime, duration}
</a>
 * Checks if audience is not booked for specified period and returns bool value.

<a name="open_audience">
* POST /Audience/Open/{audienceId}
</a>
 * Checks whether the user has administrator rights. Returns 401/403 status code otherwise.
 * Marks audience as available for booking.

<a name="close_audience">
* POST /Audience/Close/{audienceId}
</a>
 * Checks whether the user has administrator rights. Returns 401/403 status code otherwise.
 * Marks audience as unavailable for booking.
 * Removes all events in this room and sends email notifications to events authors and participants.

 <a name="get_edit_form">
* GET /Audience/Edit/{audienceId}
 </a>
  * Checks whether the user has administrator rights. Returns 401/403 status code otherwise.
  * Returns partial view with edit form.
  * Binded with
[AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)

<a name="save_audience">
* POST /Audience/Save/{audienceInfoViewModel}
</a>
 * Checks whether the user has administrator rights. Returns 401/403 status code otherwise.
 * Saves changes.  
 * Binded with
 [AudienceInfoViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceInfoViewModel.cs)
