using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.RunningNumbers
{
    public class RunningNumberComponent : IRunningNumberComponent
    {
        public IAppSetting _appSetting;
        public IUnitOfWork _unitOfWork;

        public RunningNumberComponent(
            IAppSetting appSetting,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appSetting = appSetting;
        }

        public string GenerateReferenceNo(string fullFormat, string group)
        {
            if (string.IsNullOrEmpty(fullFormat))
            {
                throw new Exception("fullFormat must have value");
            }

            if (!fullFormat.Contains("{RunningNo"))
            {
                throw new Exception("fullFormat must have {RunningNo:<running no length>} keyword");
            }

            RunningNumber runningNumber = CreateRunningNumberInstance(fullFormat, group);

            var referenceNo = ReplaceRunningNo("{RunningNo}", fullFormat, runningNumber.RunningNo);

            return referenceNo;
        }

        private RunningNumber CreateRunningNumberInstance(string format, string group)
        {
            RunningNumber runningNo = _unitOfWork.Repository.FindBy<RunningNumber>(x => x.Group == group);
            if (runningNo == null)
            {
                runningNo = new RunningNumber();
                runningNo.Format = format;
                runningNo.Group = group;
                runningNo.RunningNo = 1;

                _unitOfWork.Repository.Insert(runningNo);
            }
            else
            {
                runningNo.RunningNo += 1;
                runningNo.Format = format;
            }

            _unitOfWork.Save();

            return runningNo;
        }

        private string ReplaceRunningNo(string format, string sentence, int number)
        {
            string preKeyword = format.Substring(0, format.Length - 1) + ":";
            string postKeyword = "}";
            string keywordPattern = preKeyword + "\\d+" + postKeyword;
            Regex regex = new Regex(keywordPattern);
            if (regex.IsMatch(sentence))
            {
                string osub = regex.Match(sentence).Value;
                string sub = osub.Replace(preKeyword, string.Empty);
                sub = sub.Replace(postKeyword, string.Empty);
                int runningLength = Convert.ToInt32(sub);
                string runningFormat = string.Empty;

                for (int i = 0; i < runningLength; i++)
                {
                    runningFormat += "0";
                }
                sentence = sentence.Replace(osub, string.Format("{0:" + runningFormat + postKeyword, number));
            }

            return sentence;
        }
    }
}
