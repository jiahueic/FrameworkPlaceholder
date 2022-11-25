using Newtonsoft.Json;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.Framework.Paging
{
    public class PagingRequest
    {
        //[JsonProperty("skip")]
        public int Skip { get; set; }

        //[JsonProperty("take")]
        public int Take { get; set; }

        //[JsonProperty("antiForgery")]
        public string antiForgery { get; set; }

        //[JsonProperty("requiresCounts")]
        public bool RequiresCounts { get; set; }

        //[JsonProperty("table")]
        public string Table { get; set; }

        //[JsonProperty("group")]
        public List<string> Group { get; set; }

        //[JsonProperty("select")]
        public List<string> Select { get; set; }

        //[JsonProperty("expand")]
        public List<string> Expand { get; set; }

        //[JsonProperty("sorted")]
        public List<Sort> Sorted { get; set; }

        //[JsonProperty("search")]
        public List<SearchFilter> Search { get; set; }

        //[JsonProperty("where")]
        public List<WhereFilter> Where { get; set; }

        //[JsonProperty("aggregates")]
        public List<Aggregate> Aggregates { get; set; }
    }

    public class Sort
    {
        public Sort()
        {
        }

        public string Name { get; set; }
        public string Direction { get; set; }
    }

    public class SearchFilter
    {
        public SearchFilter()
        {
        }

        public List<string> Fields { get; set; }
        public string Key { get; set; }
        public string Operator { get; set; }
    }

    public class WhereFilter
    {
        public WhereFilter()
        {
        }

        public string Field { get; set; }
        public bool IgnoreCase { get; set; }
        public bool IsComplex { get; set; }
        public string Operator { get; set; }
        public string Condition { get; set; }
        public object value { get; set; }
        public List<WhereFilter> predicates { get; set; }
    }

    public class Aggregate
    {
        public Aggregate()
        {
        }

        public string Field { get; set; }
        public string Type { get; set; }
    }
}