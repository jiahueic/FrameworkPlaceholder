using AGTIV.Framework.MVC.Business.Role;
using AGTIV.Framework.MVC.Data.Context;
using AGTIV.Framework.MVC.DTO.Role;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AGTIV.Framework.MVC.Framework.Authentication;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [RoutePrefix("api/Role")]
    [Authorize]
    public class RoleController : ApiController
    {
        private readonly IRoleComponent _roleComponent;

        public RoleController(IRoleComponent roleComponent)
        {
            _roleComponent = roleComponent;
        }

        public IHttpActionResult Get()
        {
            var result = _roleComponent.Get();

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(RoleAssignment roleAssignment)
        {
            AppDbContext context = new AppDbContext();
            var userManager = new AppUserManager(new AppUserStore(context));
            foreach(var role in roleAssignment.SelectedRole)
            {
                await userManager.AddToRoleAsync(roleAssignment.UserId, role);
            }

            var roles = _roleComponent.Get();
            roles = roles.Except(roles.Where(c => roleAssignment.SelectedRole.Contains(c.Name)));
            var excludedRoles = roles.Select(c => c.Name).ToArray();

            foreach(var role in excludedRoles)
            {
                await userManager.RemoveFromRoleAsync(roleAssignment.UserId, role);
            }         

            return Ok();
        }
    }
}
