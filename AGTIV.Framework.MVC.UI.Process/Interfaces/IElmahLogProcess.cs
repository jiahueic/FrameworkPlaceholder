using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.ViewModel.ElmahLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IElmahLogProcess
    {
        PagedList<ElmahErrorVM> GetPaged(PagingRequest paging);

        ElmahErrorVM GetError(Guid errorId);
    }
}
