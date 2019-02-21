using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class ListCommentNewsDTO
    {
        public int ID { get; set; }

        public int? PeopleID { get; set; }

        public int? NewsID { get; set; }

        public string Content { get; set; }

        public string CreateDate { get; set; }

        public bool? Status { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool Status2 { get; set; }

    }
}
