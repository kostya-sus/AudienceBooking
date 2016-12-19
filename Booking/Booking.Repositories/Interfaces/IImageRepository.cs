using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Booking.Repositories.Interfaces
{
    public interface IImageRepository
    {
        void UploadImage(Stream stream, string imageName);

        Task UploadImageAsync(Stream stream, string imageName);

        string GetImageUri(string imageName);
    }
}