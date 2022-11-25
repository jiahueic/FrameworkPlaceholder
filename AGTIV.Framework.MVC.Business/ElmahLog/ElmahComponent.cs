using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Entities.ElmahLog;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.Paging;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.ElmahLog
{
    public class ElmahComponent : IElmahComponent
    {
        private IAppSetting _appSetting;
        private IUnitOfWork _unitOfWork;
        public ElmahComponent(IAppSetting appSetting, IUnitOfWork unitOfWork)
        {
            _appSetting = appSetting;
            _unitOfWork = unitOfWork;
        }

        public PagedList<Elmah_Error> GetElmahErrors(PagingRequest paging)
        {
            IQueryable<Elmah_Error> data = _unitOfWork.Repository.GetQuery<Elmah_Error>();
            var pagedList = PagingHelper.GetPagedList(data, paging);

            return pagedList;
        }

        public List<Elmah_Error> GetElmahErrors()
        {
            IQueryable<Elmah_Error> data = _unitOfWork.Repository.GetQuery<Elmah_Error>();
            return data.ToList();
        }

        public Elmah_Error GetElmahError(Guid errorId)
        {
            return _unitOfWork.Repository.Get<Elmah_Error>(x => x.ErrorId == errorId)?.FirstOrDefault();
        }
    }
}
