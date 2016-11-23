namespace Booking.Services.Interfaces
{
    public interface IContactService
    {
        bool SendMessage(string name, string surname, string email, string text);
    }
}
