using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class DislikeHistoryDTO
    {
        public DateTime? CreateDate { get; set; }

        public string TitlePost { get; set; }

        public int? PostID { get; set; }

        public string TitleNews { get; set; }

        public int? NewsID { get; set; }
    }
}
