using System.Collections.Generic;

namespace AGTIV.Framework.MVC.Framework.Paging
{
    public class PagedList<T>
    {
        public List<T> Result { get; set; }

        public int TotalCount { get; set; }
    }
}