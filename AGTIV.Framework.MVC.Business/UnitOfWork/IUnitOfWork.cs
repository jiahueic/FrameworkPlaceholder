using AGTIV.Framework.MVC.Business.Workflows;
using AGTIV.Framework.MVC.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository Repository { get; }

        IWorkflowRepository WorkflowRepository { get; }

        int Save();
    }
}
