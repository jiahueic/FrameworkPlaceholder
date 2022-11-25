using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace AGTIV.Framework.MVC.Business.User
{
    public class UserComponent : IUserComponent
    {
        private readonly IAppSetting _appSetting;
        //private readonly IIdentityManager _identityProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UserComponent(IAppSetting appSetting, IUnitOfWork unitOfWork)
        {
            _appSetting = appSetting;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDto> Get()
        {
            var roleEntities = _unitOfWork.Repository.Get<AppRole>().ToList();
            var userList = _unitOfWork.Repository.GetAll<UserProfile>(c => c.AppUser.Roles);
            var dtoList = userList.Select(x => MapToDto(x, roleEntities));
            return dtoList;
        }

        public UserProfile Get(Guid id)
        {
            var user = _unitOfWork.Repository.GetByID<UserProfile>(id, c => c.AppUser.Roles);
            var roles = _unitOfWork.Repository.Get<AppRole>();
            List<string> roleList = new List<string>();
            if (user.AppUser != null)
            {
                foreach (var role in user.AppUser.Roles)
                {
                    roleList.Add(roles.Where(c => c.Id == role.RoleId).SingleOrDefault().Name);
                }
                user.Roles = roleList.ToArray();
            }
            user.Image = _unitOfWork.Repository.Get<Image>(c => c.UserProfileId == user.Id).SingleOrDefault();

            return user;
        }

        public UserProfile GetByAppUserId(Guid id)
        {
            var user = _unitOfWork.Repository.Get<UserProfile>(c => c.Id == id).SingleOrDefault();
            return user;
        }

        public void CreateUserProfile(UserProfile userProfile)
        {
            _unitOfWork.Repository.Insert<UserProfile>(userProfile);
            var result = _unitOfWork.Save();
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            using(TransactionScope trans = new TransactionScope())
            {

                var tempUserProfile = _unitOfWork.Repository.GetByID<UserProfile>(userProfile.Id);

                tempUserProfile.FullName = userProfile.FullName;
                tempUserProfile.MobileNo = userProfile.MobileNo;
                tempUserProfile.NewNRIC = userProfile.NewNRIC;
                tempUserProfile.Department = userProfile.Department;
                tempUserProfile.Manager = userProfile.Manager;
                tempUserProfile.Address = userProfile.Address;
                tempUserProfile.PostCode = userProfile.PostCode;
                tempUserProfile.State = userProfile.State;
                tempUserProfile.Country = userProfile.Country;
                tempUserProfile.CalendarProfile_Id = userProfile.CalendarProfile_Id;

                _unitOfWork.Repository.Update<UserProfile>(tempUserProfile);

                var image = _unitOfWork.Repository.GetByID<Image>(userProfile.Image.Id);

                if (image != null)
                {
                    image.FileBytes = userProfile.Image.FileBytes;
                    image.Extension = userProfile.Image.Extension;
                    image.ContentType = MimeMapping.GetMimeMapping("dummy" + image.Extension);
                    _unitOfWork.Repository.Update<Image>(image);
                }
                else
                {
                    image = new Image();

                    image.UserProfileId = userProfile.Id;
                    image.FileBytes = userProfile.Image.FileBytes;
                    image.ContentType = userProfile.Image.ContentType;
                    image.ContentType = MimeMapping.GetMimeMapping("dummy" + image.Extension);
                    _unitOfWork.Repository.Insert<Image>(image);
                }

                _unitOfWork.Save();

                trans.Complete();
            }
        }

        public void UploadProfilePicture(Image image)
        {
            image.UserProfileId = UserAccessControl.GetCurrentUserId();
            var existingImage = _unitOfWork.Repository.Get<Image>(c => c.UserProfileId == image.UserProfileId).SingleOrDefault();
            if (existingImage == null)
            {
                _unitOfWork.Repository.Insert(image);

            }
            else
            {
                existingImage.Title = image.Title;
                existingImage.Extension = image.Extension;
                existingImage.ContentType = image.ContentType;
                existingImage.FileBytes = image.FileBytes;

                _unitOfWork.Repository.Update(existingImage);
            }

            _unitOfWork.Save();
        }

        private UserDto MapToDto(UserProfile entity, List<AppRole> roleEntities)
        {
            return new UserDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Username = entity.AppUser.UserName,
                EmailAddress = entity.EmailAddress,
                MobileNo = entity.EmailAddress,
                NewNRIC = entity.NewNRIC,
                Address = entity.Address,
                PostCode = entity.PostCode,
                State = entity.State,
                Country = entity.Country,
                Department = entity.Department,
                Manager = entity.Manager,
                CalendarProfile_Id = entity.CalendarProfile_Id,
                Roles = roleEntities.Where(x => entity.AppUser.Roles != null 
                        ? entity.AppUser.Roles.Select(r => r.RoleId).Contains(x.Id)
                        : false)
                    ?.Select(x => x.Name)
                    .ToArray(),
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
                Status = entity.Status
            };
        }
    }
}
