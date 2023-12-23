using CloudinaryDotNet.Actions;

namespace ModelShop.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string url);
    }
}
