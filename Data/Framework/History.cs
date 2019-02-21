namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History")]
    public partial class History
    {
        public int ID { get; set; }

        public int? CategoryHistoryID { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? PeopleID { get; set; }

        public virtual CategoryHistory CategoryHistory { get; set; }

        public virtual Person Person { get; set; }
    }
}
