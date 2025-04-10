using DBManager.DbModels;
using DBManager.PaggingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Pagging.PaggingClasses
{
    public class PaggingPost:PaggingClassBase
    {
        public List<Post> Posts { get; set; }
        public string SearchedTitle {  get; set; }
        public string SearchedContent { get; set; }
        public string SearchedAuthor { get; set; }
    }
}
