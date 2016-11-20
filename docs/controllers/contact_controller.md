# Contact controller

## Requests

<a name="get_index">
* GET /Contact/Index
</a>
 * Returns view with
 [ContactViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Contact/ContactViewModel.cs) form

<a name="post_index">
* POST /Contact/Index/{contactViewModel}
</a>
 * Validates received contactViewModel and sends emails to admins.
 * Returns information about operation success or fail.
