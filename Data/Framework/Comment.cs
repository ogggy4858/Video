namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public int ID { get; set; }

        public int? PeopleID { get; set; }

        public int? PostID { get; set; }

        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        public virtual Person Person { get; set; }

        public virtual Post Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
