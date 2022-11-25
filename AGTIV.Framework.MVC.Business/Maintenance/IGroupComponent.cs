using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.Business.Maintenance
{
    public interface IGroupComponent
    {
        void Create(GroupDto group);

        void Update(GroupDto group);

        void Delete(Guid id);

        GroupDto Get(Guid id);

        List<GroupDto> GetAll();

        PagedList<GroupDto> GetPaged(PagingRequest paging);
    }
}