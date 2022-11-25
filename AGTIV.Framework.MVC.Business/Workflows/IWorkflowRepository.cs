using AGTIV.Framework.MVC.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Workflows
{
    public interface IWorkflowRepository
    {
        d_tblStep GetStepTemplateByStepId(int stepId);

        d_tblAction GetActionTemplateByActionId(int actionId);

        List<d_tblStep> GetEditableStepTemplate();

        List<d_tblAction> GetEditableActionTemplate();

        void UpdateStepTemplate(d_tblStep step);

        void UpdateActionTemplate(d_tblAction action);
    }
}
