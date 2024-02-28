using Azure.Storage.Blobs;

using Gesd.Data.Settings;
using Gesd.Features.Contrats.Repositories;

using Microsoft.AspNetCore.Http;

namespace Gesd.Data.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private BlobServiceClient BlobServiceClient { get; }
        private BlobContainerClient BlobContainerClient { get; }

        private readonly FileSettings _fichierSetting;

        public BlobRepository(FileSettings fichierSetting)
        {
            _fichierSetting = fichierSetting;
            BlobServiceClient = new BlobServiceClient(_fichierSetting.ChaineDeConnectionBlob);
            BlobContainerClient = BlobServiceClient.GetBlobContainerClient(_fichierSetting.BlobContainerName);
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

        public async Task<bool> Delete(string filePath)
        {
            try
            {
                if (_fichierSetting.Environment != ToolsNeeds.LOCAL)
                {
                    return await SupprimerUnFichierDansLeBlob(filePath);
                }
                else
                {
                    return SupprimerUnFichierDansLeDossierLocal(filePath);
                }
            }
            catch (Exception ex)
            {
                // Gérez les exceptions ici
                Console.WriteLine($"Une erreur s'est produite lors de la suppression du blob : {ex.Message}");
                return false;
            }
        }


        #region PRIVATE FUNCTION

        private async Task<(string fileName, string filePath)> saveFileInBlobStorage(IFormFile file)
        {
            string fileName = Path.GetFileName(file.FileName);
            var blobClient = BlobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                await using (var memoryStream = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(memoryStream);
                    memoryStream.Position = 0;

                    await using var newMemoryStream = new MemoryStream();
                    await file.CopyToAsync(newMemoryStream);
                    newMemoryStream.Position = 0;

                    if (memoryStream.Length == newMemoryStream.Length && memoryStream.ToArray().SequenceEqual(newMemoryStream.ToArray()))
                        return (fileName, blobClient.Uri.ToString());
                }

                int suffix = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string fileExtension = Path.GetExtension(fileName);

                do
                {
                    fileName = $"{fileNameWithoutExtension}_{suffix}{fileExtension}";
                    blobClient = BlobContainerClient.GetBlobClient(fileName);
                    suffix++;
                } while (await blobClient.ExistsAsync());
            }

            await using (var fileStream = file.OpenReadStream())
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

        private bool SupprimerUnFichierDansLeDossierLocal(string filePath)
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

        private async Task<bool> SupprimerUnFichierDansLeBlob(string filePath)
        {
            // Obtenez une référence à un blob existant
            var blobClient = BlobContainerClient.GetBlobClient(_fichierSetting.BlobContainerName);

            // Supprimez le blob
            await blobClient.DeleteIfExistsAsync();
            return true;
        }

        public Task<Entite.File> Get()
        {
            throw new NotImplementedException();
        }

        #endregion PRIVATE FUNCTION
    }
}
