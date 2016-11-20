# Audience controller

## Requests
<a name="get_audienceinfo">
* GET /Audience/GetAudienceInfo/{audienceId}
</a>
 * Returns JSON (serialized [AudienceViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Audience/AudienceViewModel.cs)).

<a name="get_audience">
* GET /Audience/{audienceId}
</a>

<a name="is_free">
GET /Room/IsFree/{roomId, dateTime, duration}
</a>
 * Checks if room is not booked for specified period and returns bool value.
