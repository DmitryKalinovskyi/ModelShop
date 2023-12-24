using CloudinaryDotNet.Actions;
using Microsoft.Extensions.FileProviders;

namespace ModelShop.Services
{
    public interface IFileService
    {
        public Task<UploadResult> AddFileAsync(IFormFile fileInfo);

        public Task<DeletionResult> DeleteFileAsync(string fileId);
    }
}
