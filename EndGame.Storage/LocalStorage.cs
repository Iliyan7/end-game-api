using EndGame.Storage.Contracts;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace EndGame.Storage
{
    public class LocalStorage : IStorageProvider, ILocalStorage
    {
        //private readonly IHostingEnvironment _env;
        private readonly string storageFolder;

        public LocalStorage(IHostingEnvironment env)
        {
            //_env = env;
            storageFolder = Path.Combine(env.WebRootPath, "images", "games");
        }

        public string GetLocalPath(string key) => Path.Combine(storageFolder, key);

        public async Task<Stream> DownloadAsync(string key)
        {
            var pathToDownload = Path.Combine(storageFolder, key);

            if (!File.Exists(pathToDownload))
            {
                return null;
            }

            var downloadStream = File.OpenRead(pathToDownload);

            return await Task.FromResult(downloadStream);
        }

        public async Task UploadAsync(Stream uploadStream, string key)
        {
            if (!Directory.Exists(storageFolder))
            {
                Directory.CreateDirectory(storageFolder);
            }

            var outputPath = Path.Combine(storageFolder, key);

            using (FileStream outputStream = File.Create(outputPath))
            {
                await uploadStream.CopyToAsync(outputStream);
            }
        }

        public async Task RemoveAsync(string key)
        {
            var pathToDelete = Path.Combine(storageFolder, key);

            await Task.Run(() =>
            {
                if (File.Exists(pathToDelete))
                {
                    File.Delete(pathToDelete);
                }
            });
        }
    }
}
