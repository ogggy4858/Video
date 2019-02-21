namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reply")]
    public partial class Reply
    {
        public int ID { get; set; }

        public int? CommentID { get; set; }

        public int? PeopleID { get; set; }

        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual Person Person { get; set; }
    }
}
