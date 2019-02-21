using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class ViewByPostCategoryDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? PeopleID { get; set; }

        public bool? Status { get; set; }

        public int? PostCategoryID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}
