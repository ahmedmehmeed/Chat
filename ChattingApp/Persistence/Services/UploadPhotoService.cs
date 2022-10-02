using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.IServices;
using ChattingApp.Resource.User;
using CloudinaryDotNet.Actions;
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
            var user = await appDbContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == uploadphoto.UserId);
            var result = await photoRepository.AddPhotoAsync(uploadphoto.photo);
            if (result.Error != null) return 0;
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);
            appDbContext.Entry(user).State = EntityState.Modified;
            return await appDbContext.SaveChangesAsync();
             

               

        }

        //public async Task<int> UploadPhotoAsync(Uploadphoto uploadphoto)
        //{
        //    var user =await  userRepository.GetUserByIdAsync(uploadphoto.UserId);
        //    var result = await photoRepository.AddPhotoAsync(uploadphoto.photo);
        //    if (result.Error != null) return 0;
        //    var photoDto = new PhotoDto
        //    {
        //        Url = result.SecureUrl.AbsoluteUri,
        //        PublicId = result.PublicId
        //    };
        //    if (user.PhotoDto.Count == 0)
        //    {
        //        photoDto.IsMain = true;
        //    }

        //    user.PhotoDto.Add(photoDto);
        //   var  userMapped= mapper.Map<UserUpdateDto>(user);
        //    // exception in mapping from UserUpdateDto ==> AppUsers in UpdateUserAsync()
        //    return await userRepository.UpdateUserAsync(userMapped);

        //}


    }
}
