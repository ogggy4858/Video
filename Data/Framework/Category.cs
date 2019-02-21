namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
