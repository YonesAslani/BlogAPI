using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.PaggingClasses
{
    public class PaggingClassBase
    {
        public int TotalPageCount {  get; set; }
        public int StartPage {  get; set; }
        public int EndPage { get; set; }
        public int TotalPosts {  get; set; }
        public int PageSize { get; set; }
        public int PageId { get; set; }

        public void GeneratPagging(IQueryable<object> list,int pagesize,int pageid)
        {
            TotalPageCount = (list.Count() % pagesize == 0) ? list.Count() / pagesize : (list.Count() / pagesize) + 1;
            StartPage=(pageid-2<=0)?1:pageid-2;
            EndPage=(pageid+2>TotalPageCount)?TotalPageCount:pageid+2;
            PageId=pageid;
            PageSize=pagesize;
            TotalPosts = list.Count();
        }
    }
}
