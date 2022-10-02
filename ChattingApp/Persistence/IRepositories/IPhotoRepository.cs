using CloudinaryDotNet.Actions;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IPhotoRepository
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
