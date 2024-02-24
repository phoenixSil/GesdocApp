using Azure.Storage.Blobs;
using Gesd.Data.Settings;
using Gesd.Features.Contrats.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Data.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private readonly FileSettings _fichierSetting;

        public BlobRepository(FileSettings fichierSetting)
        {
            _fichierSetting = fichierSetting;
        }
        public async Task<(string fileName, string filePath)> Add(IFormFile file)
        {
            
            if (_fichierSetting.Environment == ToolsNeeds.LOCAL)
            {
                return await saveFileInLocalStorage(file).ConfigureAwait(false);
            }

            else
            {
                return await saveFileInBlobStorage(file).ConfigureAwait(false);
            }
        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }


        #region PRIVATE FUNCTION

        private async Task<(string fileName, string filePath)> saveFileInBlobStorage(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_fichierSetting.ChaineDeConnectionBlob);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_fichierSetting.BlobContainerName);

            string fileName = Path.GetFileName(file.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var newMemoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(newMemoryStream);
                        newMemoryStream.Position = 0;

                        if (memoryStream.Length == newMemoryStream.Length && memoryStream.ToArray().SequenceEqual(newMemoryStream.ToArray()))
                            return (fileName, blobClient.Uri.ToString());
                    }
                }

                int suffix = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string fileExtension = Path.GetExtension(fileName);

                do
                {
                    fileName = $"{fileNameWithoutExtension}_{suffix}{fileExtension}";
                    blobClient = blobContainerClient.GetBlobClient(fileName);
                    suffix++;
                } while (await blobClient.ExistsAsync());
            }

            using (var fileStream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(fileStream, true);
            }

            return (fileName, blobClient.Uri.ToString());
        }

        private async Task<(string fileName, string filePath)> saveFileInLocalStorage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_fichierSetting.CheminLocalFichier, _fichierSetting.FolderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            int suffix = 1;
            while (System.IO.File.Exists(filePath))
            {
                fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{suffix}{Path.GetExtension(file.FileName)}";
                filePath = Path.Combine(uploadsFolder, fileName);
                suffix++;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return (fileName, filePath);
        }

        #endregion
    }
}
