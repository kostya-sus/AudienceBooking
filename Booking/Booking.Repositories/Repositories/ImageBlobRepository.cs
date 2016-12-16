using System;
using System.IO;
using System.Threading.Tasks;
using Booking.Repositories.Interfaces;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Booking.Repositories.Repositories
{
    public class ImageBlobRepository : IImageRepository
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public ImageBlobRepository()
        {
            _storageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            _storageContainerName = CloudConfigurationManager.GetSetting("StorageContainerName");
        }


        public void UploadImage(Stream stream, string imageName)
        {
            var container = GetContainer();

            CloudBlockBlob blob = container.GetBlockBlobReference(imageName);

            using (stream)
            {
                blob.UploadFromStream(stream);
            }
        }

        public async Task UploadImageAsync(Stream stream, string imageName)
        {
            CloudBlockBlob blockBlob = GetContainer().GetBlockBlobReference(imageName);

            using (stream)
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }
        }

        public string GetImageUri(string imageName)
        {
            var container = GetContainer();
            var blob = container.GetBlockBlobReference(imageName);

            return blob.Uri.ToString();
        }

        private CloudBlobContainer GetContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_storageContainerName);
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            container.CreateIfNotExists();
            return container;
        }
    }
}