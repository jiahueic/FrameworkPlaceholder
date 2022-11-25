using AGTIV.Framework.MVC.Business.Maintenance;
using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Web.Http;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Group")]
    public class GroupController : ApiController
    {
        private readonly IGroupComponent _groupComponent;

        public GroupController(IGroupComponent groupComponent)
        {
            _groupComponent = groupComponent;
        }

        [Route("~/api/Groups")]
        public IHttpActionResult Get()
        {
            var groups = _groupComponent.GetAll();
            return Ok(groups);
        }

        [Route("~/api/Groups")]
        [HttpPost]
        public IHttpActionResult GetAll(PagingRequest paging)
        {
            var groups = _groupComponent.GetPaged(paging);
            return Ok(groups);
        }

        [Route("{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var group = _groupComponent.Get(id);
            return Ok(group);
        }

        public IHttpActionResult Post(GroupDto group)
        {
            _groupComponent.Create(group);
            return Ok(true);
        }

        [Route("{id}")]
        public IHttpActionResult Put(Guid id, GroupDto group)
        {
            _groupComponent.Update(group);
            return Ok(true);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            _groupComponent.Delete(id);
            return Ok(true);
        }
    }
}