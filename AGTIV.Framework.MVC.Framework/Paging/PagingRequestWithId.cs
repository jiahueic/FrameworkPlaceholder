using Newtonsoft.Json;
using System;

namespace AGTIV.Framework.MVC.Framework.Paging
{
    public class PagingRequestWithId : PagingRequest
    {
        public PagingRequestWithId(PagingRequest paging) : base()
        {
            Skip = paging.Skip;
            Take = paging.Take;
            antiForgery = paging.antiForgery;
            RequiresCounts = paging.RequiresCounts;
            Table = paging.Table;
            Group = paging.Group;
            Select = paging.Select;
            Expand = paging.Expand;
            Sorted = paging.Sorted;
            Search = paging.Search;
            Where = paging.Where;
            Aggregates = paging.Aggregates;
        }

        public Guid QueryId { get; set; }
    }
}