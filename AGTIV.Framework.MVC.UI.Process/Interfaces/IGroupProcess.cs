using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IGroupProcess
    {
        void Create(GroupFormVM vm);

        bool Update(GroupFormVM vm);

        void Delete(Guid id);

        GroupFormVM Get(Guid id);

        List<GroupVM> GetAll();

        PagedList<GroupVM> GetAll(PagingRequest paging);
    }
}