using AGTIV.Framework.General;
using AGTIV.Framework.MVC.Business.Workflows;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Data.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private SQLHelper _sqlHelper = new SQLHelper(ConfigurationManager.ConnectionStrings[ConstantHelper.ConnString.Default].ToString());

        public d_tblStep GetStepTemplateByStepId(int stepId)
        {
            var step = new d_tblStep();

            try
            {
                string sql = @"SELECT TOP 1 * FROM d_tblStep WHERE StepId = @StepId";
                List<string> parameters = new List<string>();
                List<object> values = new List<object>();

                parameters.Add("@StepId");
                values.Add(stepId);

                var dt = _sqlHelper.SQLExecuteAsDataSet(true, sql, parameters, values);

                if (dt != null && dt.Rows.Count > 0)
                {
                    step = dt.Rows[0].ToObject<d_tblStep>();
                }
            }
            catch (Exception) { }

            return step;
        }

        public d_tblAction GetActionTemplateByActionId(int actionId)
        {
            var action = new d_tblAction();

            try
            {
                string sql = @"SELECT TOP 1 * FROM d_tblAction WHERE ActionId = @ActionId";
                List<string> parameters = new List<string>();
                List<object> values = new List<object>();

                parameters.Add("@ActionId");
                values.Add(actionId);

                var dt = _sqlHelper.SQLExecuteAsDataSet(true, sql, parameters, values);

                if (dt != null && dt.Rows.Count > 0)
                {
                    action = dt.Rows[0].ToObject<d_tblAction>();
                }
            }
            catch (Exception) { }

            return action;
        }

        public List<d_tblAction> GetEditableActionTemplate()
        {
            var actions = new List<d_tblAction>();

            try
            {
                string sql = $@"SELECT * FROM d_tblAction WHERE ActionName IN ('{ConstantHelper.Workflow.d_tblAction.ActionName.Approve}')";
                List<string> parameters = new List<string>();
                List<object> values = new List<object>();

                var dt = _sqlHelper.SQLExecuteAsDataSet(true, sql, parameters, values);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var action = new d_tblAction();
                        action = dr.ToObject<d_tblAction>();
                        actions.Add(action);
                    }
                }
            }
            catch (Exception) { }

            return actions;
        }

        public List<d_tblStep> GetEditableStepTemplate()
        {
            var steps = new List<d_tblStep>();

            try
            {
                string sql = $@"SELECT * FROM d_tblStep WHERE InternalStepName NOT IN ('{ConstantHelper.Workflow.d_tblStep.InternalStepName.Error}', '{ConstantHelper.Workflow.d_tblStep.InternalStepName.GoTo}', '{ConstantHelper.Workflow.d_tblStep.InternalStepName.Terminated}', '{ConstantHelper.Workflow.d_tblStep.InternalStepName.Start}')";
                List<string> parameters = new List<string>();
                List<object> values = new List<object>();

                var dt = _sqlHelper.SQLExecuteAsDataSet(true, sql, parameters, values);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var step = new d_tblStep();
                        step = dr.ToObject<d_tblStep>();
                        steps.Add(step);
                    }
                }
            }
            catch (Exception ex) 
            {
            
            }

            return steps;
        }

        public void UpdateStepTemplate(d_tblStep step)
        {
            try
            {
                List<string> field2Update = new List<string>();
                List<object> fieldValue = new List<object>();

                field2Update.Add(nameof(step.StepName));
                fieldValue.Add(step.StepName);

                if (step.EmailNotificationSubject != null)
                {
                    field2Update.Add(nameof(step.EmailNotificationSubject));
                    fieldValue.Add(step.EmailNotificationSubject);
                }

                if (step.EmailNotificationBody != null)
                {
                    field2Update.Add(nameof(step.EmailNotificationBody));
                    fieldValue.Add(step.EmailNotificationBody);
                }

                if(step.TaskURL != null)
                {
                    field2Update.Add(nameof(step.TaskURL));
                    fieldValue.Add(step.TaskURL);
                }

                if(step.DueDateDay > -1)
                {
                    field2Update.Add(nameof(step.DueDateDay));
                    fieldValue.Add(step.DueDateDay);
                }

                var result = _sqlHelper.Update("d_tblStep", field2Update, fieldValue, "StepId", step.StepID);
                
                if (!result)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateActionTemplate(d_tblAction action)
        {
            try
            {
                List<string> field2Update = new List<string>();
                List<object> fieldValue = new List<object>();

                if(action.MinSlot > -1)
                {
                    field2Update.Add(nameof(action.MinSlot));
                    fieldValue.Add(action.MinSlot);
                }

                var result = _sqlHelper.Update("d_tblAction", field2Update, fieldValue, "ActionID", action.ActionID);

                if (!result)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
