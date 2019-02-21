namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            CommentNews = new HashSet<CommentNew>();
            DislikeNews = new HashSet<DislikeNew>();
            LikeNews = new HashSet<LikeNew>();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? DisplayOrder { get; set; }

        public int? PeopleID { get; set; }

        public int? ViewCount { get; set; }

        public string Image2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentNew> CommentNews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DislikeNew> DislikeNews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikeNew> LikeNews { get; set; }

        public virtual Person Person { get; set; }
    }
}
