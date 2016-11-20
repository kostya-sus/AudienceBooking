# Profile page
* When page is loaded or user changes date on schedule table, ajax GET request to
[/Profile/Schedule/{userId,date}](controllers/profile_controller.md#get_schedule) is sent and received events rendered to the schedule table.
* If user is owner of the profile or admin, he can edit profile, ajax GET request is sent to [/Profile/Edit/{userId}](controllers/profile_controller.md#edit), received partial view with edit form replaces the div with displayed profile information. On submit, ajax POST request to [/Profile/Save/{editProfileViewModel}](controllers/profile_controller.md#save) is sent, and edit form is replaced with received in respond display view.
* If admin is editing someone\`s profile, he can add administrator role.
* Admin can delete user\`s profile via DELETE request to [/Profile/Delete/{userId}](controllers/profile_controller.md#delete). Before he deletes profile, he should confirm this operation in the dialog.
* Any user can logout from the site on this page.
