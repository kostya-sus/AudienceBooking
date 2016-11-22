# Profile controller

## Request

<a name="get_profile">
* GET /Profile/{userId}
</a>
 * Checks if user is owner of the requested account or admin.
 * Returns view, rendered using [ProfileViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Profile/ProfileViewModel.cs)

<a name="get_schedule">
* GET /Profile/Schedule/{userId,date}
</a>
 * Returns only events, created by specified user.
 * Returns JSON (serialized [DayScheduleViewModel](https://github.com/anshox/AudienceBooking/blob/contracts-definitions/Booking/Booking.Web/ViewModels/Home/DayScheduleViewModel.cs)).
 * Used to populate schedule table when [/Home/Index](#get_index) is loaded or chosen day is changed.

<a name="edit">
* GET /Profile/Edit/{userId}
</a>
 * Checks if user is owner of the profile or admin.
 * Returns Partial view with form, used to edit profile.
 * If admin is editing other profile, "Logout" button is missing and "Delete" button is rendered instead.
 Also there is an option to give the user an administrator rights.

<a name="save">
* POST /Profile/Save/{editProfileViewModel}
</a>
 * Checks if user is owner of the profile or admin.
 * Saves changes.
 * Returns Partial view with displayed profile information.

<a name="delete">
* POST /Profile/Delete/{userId}
</a>
  * Checks if user is admin.
  * Cancels all events, created by the user and sends email notifications.
  * Deletes profile.
  * Redirects to home page.
