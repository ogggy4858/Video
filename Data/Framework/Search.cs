namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Search")]
    public partial class Search
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public int? PeopleID { get; set; }

        [StringLength(500)]
        public string KeyWord { get; set; }

        public int? Count { get; set; }

        public virtual Person Person { get; set; }
    }
}
