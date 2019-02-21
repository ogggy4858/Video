using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class ListNewsDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? DisplayOrder { get; set; }

        public int? PeopleID { get; set; }

        public int? ViewCount { get; set; }

        public string Email { get; set; }

    }
}
