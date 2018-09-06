using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Api
{
    public class PagingInfo
    {
        public string TotalRecords { get; set; }
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNo
        {
            get
            {
                return Page;
            }
        }
        public int PageSize
        {
            get
            {
                return RecordsPerPage;
            }
        }
        public string OrderBy { get; set; }
        public int TotalPages { get; set; }
    }
}
