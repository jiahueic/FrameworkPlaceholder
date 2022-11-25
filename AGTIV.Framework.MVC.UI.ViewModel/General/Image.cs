using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.General
{
    public class Image : BaseViewModel
    {
        public string Title { get; set; }

        public string Extension { get; set; }

        public string TitleWithExtension
        {
            get
            {
                return Title + Extension;
            }
        }

        public string ContentType { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
