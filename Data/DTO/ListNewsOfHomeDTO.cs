using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class ListNewsOfHomeDTO
    {
        public int ID { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string CreateDate { get; set; }

        public int CommentCount { get; set; }

        public string ShortContent { get; set; }

        public string[] Tags { get; set; }
    }
}
