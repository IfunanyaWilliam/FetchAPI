using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchAPI
{
    public class FetchPage
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public List<Data> Data { get; set; } = new List<Data>();

    }

    public class Data
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string About { get; set; }
        public int Submitted { get; set; }
        public string UpdatedAt { get; set; }
        public int SubmissionCount { get; set; }
        public int CommentCount { get; set; }
        public string CtreatedAt { get; set; }
    }
}
