using AGTIV.Framework.MVC.UI.ViewModel;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using AGTIV.Framework.MVC.UI.ViewModel.User;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IUserProcess
    {
        IEnumerable<User> Get();

        User Get(Guid id);

        bool Create(CreateUserVM pVM);

        void Update(User vm);

        void Delete(Guid id);

        UserInfoViewModel Login(string username, string password/*, bool rememberMe*/);

        void ForgotPassword(string email);

        void ResetPassword(ResetPasswordVM vm);

        string ChangePassword(ChangePasswordVM vm);

        bool Signup(User user);

        void UploadProfilePicture(Image image);
    }
}
