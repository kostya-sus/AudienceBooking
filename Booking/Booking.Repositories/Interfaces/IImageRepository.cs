using System.IO;
using System.Threading.Tasks;

namespace Booking.Repositories.Interfaces
{
    public interface IImageRepository
    {
        void UploadImage(Stream stream, string imageName);

        Task UploadImageAsync(Stream stream, string imageName);

        string GetImageUri(string imageName);
    }
}