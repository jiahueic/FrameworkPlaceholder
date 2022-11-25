using AGTIV.Framework.MVC.DTO.Chart;
using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.DTO.Role;
using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.DTO.Workflow;

using AGTIV.Framework.MVC.Entities.ElmahLog;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.UI.ViewModel.Calendar;
using AGTIV.Framework.MVC.UI.ViewModel.Chart;
using AGTIV.Framework.MVC.UI.ViewModel.ElmahLog;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using AGTIV.Framework.MVC.UI.ViewModel.Role;
using AGTIV.Framework.MVC.UI.ViewModel.User;
using AGTIV.Framework.MVC.UI.ViewModel.Workflow;
using AutoMapper;
using System.Linq;

namespace AGTIV.Framework.MVC.UI.Process.Configuration
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<WorkflowConfigurationVM, WorkflowConfiguration>().ReverseMap();
                cfg.CreateMap<WorkflowMatrixVM, WorkflowMatrix>().ReverseMap();
                cfg.CreateMap<WorkflowMatrixStageVM, WorkflowMatrixStage>().ReverseMap();
                cfg.CreateMap<WorkflowStageUserVM, WorkflowStageUser>()
                .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserProfile));
                cfg.CreateMap<User, UserProfile>()
                .ReverseMap();
                cfg.CreateMap<WorkflowStageGroupVM, WorkflowStageGroup>().ReverseMap();
                cfg.CreateMap<GroupVM, Group>().ReverseMap();
                //.ForMember(dest => dest.Role, opt => opt.Ignore());

                cfg.CreateMap<WorkflowProcessVM, WorkflowProcessDto>();
                cfg.CreateMap<WorkflowHistoryVM, WorkflowHistory>();
                cfg.CreateMap<WorkflowLogVM, WorkflowLogDto>();
                cfg.CreateMap<DelegationVM, Delegation>();
                cfg.CreateMap<CreateDelegationVM, CreateDelegation>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Value))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.Value));

                cfg.CreateMap<GroupVM, GroupDto>();
                cfg.CreateMap<GroupFormVM, GroupDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserIds.Select(u => new UserDto { Id = u })))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleIds.Select(r => new Role { Id = r })))
                .ReverseMap()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.Users.Select(u => u.Id)))
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.Roles.Select(r => r.Id)));

                cfg.CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(dest => dest.RolesString, opt => opt.MapFrom(src => string.Join(", ", src.Roles)));
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<ResetPasswordVM, ResetPasswordDto>()
                .ForMember(dest => dest.NewPassword, opt => opt.MapFrom(src => src.Password));
                cfg.CreateMap<ChangePasswordVM, ChangePasswordDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUserId));

                cfg.CreateMap<CalendarProfile, CalendarProfileVM>();
                cfg.CreateMap<CalendarProfileVM, CalendarProfileDto>();
                cfg.CreateMap<CalendarProfileFormVM, CalendarProfileDto>()
                .ReverseMap()
                .ForMember(dest => dest.ParentProfileDdl, opt => opt.Ignore());
                cfg.CreateMap<HolidayVM, CalendarHolidayDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Value))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.Value))
                .ReverseMap()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToLocalTime()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToLocalTime()));

                cfg.CreateMap<Elmah_Error, ElmahErrorVM>().ReverseMap();

                cfg.CreateMap<BarChartDto, BarChartVM>();
            });
        }
    }
}
