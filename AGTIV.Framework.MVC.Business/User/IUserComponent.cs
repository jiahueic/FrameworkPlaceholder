using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.Business.User
{
    public interface IUserComponent
    {
        IEnumerable<UserDto> Get();

        UserProfile Get(Guid id);

        UserProfile GetByAppUserId(Guid id);

        void UploadProfilePicture(Image image);

        void CreateUserProfile(UserProfile userProfile);

        void UpdateUserProfile(UserProfile userProfile);
    }
}