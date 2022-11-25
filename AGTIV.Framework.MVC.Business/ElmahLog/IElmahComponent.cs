using AGTIV.Framework.MVC.Entities.ElmahLog;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.ElmahLog
{
    public interface IElmahComponent
    {
        PagedList<Elmah_Error> GetElmahErrors(PagingRequest paging);

        List<Elmah_Error> GetElmahErrors();

        Elmah_Error GetElmahError(Guid errorId);
    }
}
