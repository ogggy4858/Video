namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentNew
    {
        public int ID { get; set; }

        public int? PeopleID { get; set; }

        public int? NewsID { get; set; }

        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        public virtual News News { get; set; }

        public virtual Person Person { get; set; }
    }
}
