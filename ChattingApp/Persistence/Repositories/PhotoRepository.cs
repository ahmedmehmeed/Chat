using ChattingApp.Helper.Third_Party;
using ChattingApp.Persistence.IRepositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace ChattingApp.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Cloudinary cloudinary;

        public PhotoRepository(IOptions<CloudinarySettings> config)
        {
            var account = new Account(config.Value.CloudName,config.Value.ApiKey,config.Value.ApiSecret );
            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var UploadImageResult = new ImageUploadResult();

            if (file.Length > 0)
            {
              using  var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face")
                };
                UploadImageResult = await cloudinary.UploadAsync(uploadParams);
            }
            return UploadImageResult;
        }


        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
          var  DeletionParams = new DeletionParams(publicId);
            var result   =  await   cloudinary.DestroyAsync(DeletionParams);
            return result;
        }

    }
}
