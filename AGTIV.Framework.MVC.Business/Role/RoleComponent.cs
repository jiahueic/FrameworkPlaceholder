using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Role
{
    public class RoleComponent : IRoleComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppSetting _appSetting;

        public RoleComponent(
            IUnitOfWork unitOfWork,
            IAppSetting appSetting
            )
        {
            _unitOfWork = unitOfWork;
            _appSetting = appSetting;
        }

        public IEnumerable<AppRole> Get()
        {
            return _unitOfWork.Repository.Get<AppRole>();
        }
    }
}
