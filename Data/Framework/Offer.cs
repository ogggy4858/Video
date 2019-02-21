namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Offer")]
    public partial class Offer
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public int? PeopleID { get; set; }

        public int? PostID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
