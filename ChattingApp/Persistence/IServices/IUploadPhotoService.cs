using ChattingApp.Resource.User;

namespace ChattingApp.Persistence.IServices
{
    public interface IUploadPhotoService
    {
        Task<int> UploadPhotoAsync(Uploadphoto uploadphoto);
        Task<int> DeletePhotoAsync(string publicId);

    }
}
