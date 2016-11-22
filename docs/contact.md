# Contact page

* Contains only form, that is binded with
[ContactViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Contact/ContactViewModel.cs)
* After form validation, sends ajax POST request to [/Contact/Index/{contactViewModel}](controllers/contact_controller.md#post_index) and shows message from the respond.
