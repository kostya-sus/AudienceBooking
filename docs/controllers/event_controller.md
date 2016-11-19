# Event controller

## Requests
<a name="get_displayeventpopup">
* GET /Event/DisplayEventPopup
</a>
 * Returns 401/403 status code if user cannot view this event information(for example,
   private events).
 * Returns PartialViewResult, rendered using a [DisplayEventPopupViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventPopupViewModel.cs).
 * If user is logged in and is author of the event or admin, edit and cancel buttons are rendered too.
<a name="get_event">
* GET /Event/{eventId}
 * Returns 401/403 status code if user cannot view this event page(for example,
   private events).
 * Returns ViewResult, rendered using a
 [DisplayEventViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventViewModel.cs).
 * If user is admin or event author, edit and cancel buttons are rendered.
 Also participants emails list is passed via DisplayEventViewModel and rendered on the page.
</a>
 <a name="participate">
* POST /Event/Participate/{email}
 </a>
 <a name="get_create">
* GET /Event/Create/{eventId}
 </a>
 <a name="create">
* POST /Event/Create/{eventId}
 </a>
 <a name="cancel">
* DELETE /Event/Cancel/{eventId}
 </a>
 <a name="edit_popup">
* GET /Event/EditPopup/{eventId}
 </a>
 <a name="save">
* POST /Event/Save/{eventId}
 </a>
