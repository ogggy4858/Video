using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class PeopleListVideoDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public int? ViewCount { get; set; }

        public DateTime? CreateDatePost { get; set; }

        public bool? StatusPost { get; set; }

        public int? LikeCount { get; set; }

        public int? DislikeCount { get; set; }

        public int? CommentCount { get; set; }

    }
}
