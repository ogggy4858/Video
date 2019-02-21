namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vieww")]
    public partial class Vieww
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public int? ViewCount { get; set; }

        public int? PeopleID { get; set; }

        public int? PostID { get; set; }

        public virtual Person Person { get; set; }

        public virtual Post Post { get; set; }
    }
}
