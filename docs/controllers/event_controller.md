# Event controller

## Requests
<a name="get_displayeventpopup">
* GET /Event/DisplayEventPopup/{eventId}
</a>
 * Returns 401/403 status code if user cannot view this event information(for example,
   private events).
 * Returns PartialViewResult, rendered using a [DisplayEventPopupViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventPopupViewModel.cs).
 * If user is logged in and is author of the event or admin(specified in CanEdit field of VM), edit and cancel buttons are rendered too.

<a name="get_event">
* GET /Event/{eventId}
</a>
 * Returns 401/403 status code if user cannot view this event page(private events).
 * Returns ViewResult, rendered using a
 [DisplayEventViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventViewModel.cs).
 * If user is admin or event author(specified in CanEdit field of VM), edit and cancel buttons are rendered.
 Also participants emails list is passed via DisplayEventViewModel and rendered on the page in
 corresponding forms. This forms send DELETE requests to
 [/Event/RemoveParticipant/{participantId}](#remove_participant) on delete button click.

<a name="participate">
* POST /Event/Participate/{email}
</a>
  * Email is validated on both client and server sides.
  * Respond contains information whether he joined event or not. If not, the reason
  is displayed(email is already registered for this event or no more seats available).
  Respond contains JSON message, which is displayed in popup via JS code.
  * If user joined event successfully, email notification is sent.

<a name="get_create">
* GET /Event/Create
</a>
 * Checks if user logged in.
 * Returns empty event creation PartialView popup form.

 <a name="remove_participant">
* DELETE /Event/RemoveParticipant/{email}
 </a>
 * If the request sender is admin or event author, participant is removed.
 * Returns status code. If OK, user is removed from list on the client side.

 <a name="get_create_audience_id">
* GET /Event/Create/{audienceId, eventDateTime, duration}
 </a>
  * Checks if user is authorized.
  * Returns PartialView with specified parameters.

 <a name="create">
* POST /Event/Create/{createEditEventViewModel}
 </a>
 * Checks if user is authorized.
 * Creates new event if audience is free for specified time.
 * Sends respond with notification about success or fail.

 <a name="cancel">
* DELETE /Event/Cancel/{eventId}
 </a>
 * Checks if user can perform this operation.
 * Deletes event.
 * Notifies participants via emails about event cancelation.

 <a name="edit_popup">
* GET /Event/EditPopup/{eventId}
 </a>
  * Similar to [/Event/Create/{audienceId, eventDateTime, duration}](controllers/event_controller.md#get_create_audience_id),
  but with filled form fields and with cancel and save buttons.

 <a name="save">
* POST /Event/Save/{createEditEventViewModel}
 </a>
 * Checks whether the user has rights to edit posted event.
 * Returns respond with message about operation success or fail.
