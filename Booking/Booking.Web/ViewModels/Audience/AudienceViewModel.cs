namespace Booking.Web.ViewModels.Audience
{
    public class AudienceViewModel
    {
        public AudiencesNamesViewModel AudiencesNames { get; set; }

        public bool IsAdmin { get; set; }

        public AudienceInfoViewModel SelectedAudience { get; set; }
    }
}