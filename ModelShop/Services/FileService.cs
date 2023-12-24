using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using ModelShop.Helpers;

namespace ModelShop.Services
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;
        public FileService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.APIKey,
                config.Value.APISecret
                );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<UploadResult> AddFileAsync(IFormFile fileInfo)
        {
            var uploadResult = new RawUploadResult();

            if (fileInfo.Length > 0)
            {
                using var stream = fileInfo.OpenReadStream();

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(fileInfo.FileName, stream),
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeleteFileAsync(string fileId)
        {
            var deleteParams = new DeletionParams(fileId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
