using System.IO;
using System.Threading.Tasks;

namespace EndGame.Storage.Contracts
{
    public interface IStorageProvider
    {
        Task<Stream> DownloadAsync(string key);

        Task UploadAsync(Stream uploadStream, string key);

        Task RemoveAsync(string key);
    }
}
