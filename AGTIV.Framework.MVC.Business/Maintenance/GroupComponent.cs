using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.DTO.Role;
using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGTIV.Framework.MVC.Business.Maintenance
{
    public class GroupComponent : IGroupComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(GroupDto group)
        {
            var currentUserId = UserAccessControl.GetCurrentUserId();
            var data = new Group
            {
                Name = group.Name,
                CreatedBy = currentUserId,
                ModifiedBy = currentUserId,
            };

            AttachUsersInGroup(group, data);
            AttachRolesInGroup(group, data);
            _unitOfWork.Repository.Insert(data);
            _unitOfWork.Save();
        }

        public void Update(GroupDto group)
        {
            var data = _unitOfWork.Repository.GetByID<Group>(group.Id);
            data.Name = group.Name;
            data.ModifiedBy = UserAccessControl.GetCurrentUserId();
            data.ModifiedOn = DateTime.Now;
            AttachUsersInGroup(group, data);
            AttachRolesInGroup(group, data);
            _unitOfWork.Repository.Update(data);
            _unitOfWork.Save();
        }

        // TODO: Add delete dependency checking
        public void Delete(Guid id)
        {
            _unitOfWork.Repository.Delete<Group>(id);
            _unitOfWork.Save();
        }

        public GroupDto Get(Guid id)
        {
            var data = _unitOfWork.Repository.GetByID<Group>(id);
            var group = MapToGroupDto(data);
            return group;
        }

        public List<GroupDto> GetAll()
        {
            var data = _unitOfWork.Repository.GetAll<Group>();
            var groups = data.Select(g => MapToGroupDto(g)).ToList();
            return groups;
        }

        public PagedList<GroupDto> GetPaged(PagingRequest paging)
        {
            IQueryable<Group> data = _unitOfWork.Repository.GetQuery<Group>(false);
            var pagedList = PagingHelper.GetPagedList(data, paging);
            var groups = new PagedList<GroupDto>()
            {
                Result = pagedList.Result.Select(x => MapToGroupDto(x)).ToList(),
                TotalCount = pagedList.TotalCount
            };            
            return groups;
        }

        private void AttachUsersInGroup(GroupDto group, Group data)
        {
            data.UserProfiles = new List<UserProfile>();
            var userIds = group.Users.Select(u => u.Id);
            var users = _unitOfWork.Repository.Get<UserProfile>(u => userIds.Contains(u.Id));

            foreach(var user in users)
            {
                data.UserProfiles.Add(user);
                _unitOfWork.Repository.Attach(user);
            }
        }

        private void AttachRolesInGroup(GroupDto group, Group data)
        {
            data.AppRoles = new List<AppRole>();
            var roleIds = group.Roles.Select(r => r.Id);
            var roles = _unitOfWork.Repository.Get<AppRole>(r => roleIds.Contains(r.Id));

            foreach(var role in roles)
            {
                data.AppRoles.Add(role);
                _unitOfWork.Repository.Attach(role);
            }
        }

        private GroupDto MapToGroupDto(Group data)
        {
            return new GroupDto
            {
                Id = data.Id,
                Name = data.Name,
                Users = data.UserProfiles.Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName
                }).ToList(),
                Roles = data.AppRoles.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };
        }
    }
}