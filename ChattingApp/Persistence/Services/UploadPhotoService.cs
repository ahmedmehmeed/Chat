using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.IServices;
using ChattingApp.Resource.User;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Persistence.Services
{
    public class UploadPhotoService : IUploadPhotoService
    {
        private readonly IUserRepository userRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly IMapper mapper;
        private readonly AppDbContext appDbContext;

        public UploadPhotoService(IUserRepository userRepository,IPhotoRepository photoRepository,IMapper mapper, AppDbContext appDbContext)
        {
            this.userRepository = userRepository;
            this.photoRepository = photoRepository;
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }
        public async Task<int> UploadPhotoAsync(Uploadphoto uploadphoto)
        {
            var user = await userRepository.GetUserByIdAsync(uploadphoto.UserId);
            var result = await photoRepository.AddPhotoAsync(uploadphoto.photo);
            if (result.Error != null) return 0;
            var PhotoDto = new PhotoDto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (user.PhotoDto.Count == 0)
            {
                PhotoDto.IsMain = true;
            }

            user.PhotoDto.Add(PhotoDto);
            //var userMapped= mapper.Map<UserUpdateDto>(user);
            // var rowsEffected=  await userRepository.UpdateUserAsync(userMapped);
            var userMapped= mapper.Map<AppUsers>(user);
            appDbContext.Entry(userMapped).State = EntityState.Modified;
            return await appDbContext.SaveChangesAsync();
            
        }
    }
}
