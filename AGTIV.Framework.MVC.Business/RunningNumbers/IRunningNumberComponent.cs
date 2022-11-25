using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.RunningNumbers
{
    public interface IRunningNumberComponent
    {
        /// <summary>
        /// fullformat (format of referenceno e.g. {WorkPackage}-{AssessmentAssignedDate}-{RunningNo:3});
        /// group (running no will be reset based on grouping);
        /// </summary>
        /// <param name="fullFormat"></param>
        /// <param name="prefixFormat"></param>
        /// <returns></returns>
        string GenerateReferenceNo(string fullFormat, string prefixFormat);
    }
}
