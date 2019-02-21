using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class CommentDTO
    {

        // id comment
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string DateComment { get; set; }

        public string Date { get; set; }

        public string ContentComment { get; set; }

        public bool Status { get; set; }


    }
}
