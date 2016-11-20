# Event page

___

## Description
* This page has two states - display and edit.
* Edit available only for event author or admin.
* Display view rendered using
[DisplayEventViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/DisplayEventViewModel.cs), edit view rendered using
[EventEditViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Event/EventEditViewModel.cs)
* If user is author of event or admin, he can remove participants
(via [/Event/RemoveParticipant/{email}](controllers/event_controller.md#remove_participant))
on both display and edit views.
* In display state on rooms map there is a route displayed.
* In edit state route is not displayed, but user can choose room by clicking on the room map.
* When user changes audience or booking period in edit state, ajax GET request is sent to
[/Room/IsFree/{roomId, dateTime, duration}](controllers/audience_controller.md#is_free). If respond contains false, corresponding message is displayed.
