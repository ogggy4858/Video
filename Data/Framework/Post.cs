namespace Data.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
            DisLikes = new HashSet<DisLike>();
            Likees = new HashSet<Likee>();
            Viewws = new HashSet<Vieww>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? PeopleID { get; set; }

        public bool? Status { get; set; }

        public int? PostCategoryID { get; set; }

        [StringLength(500)]
        public string Poster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DisLike> DisLikes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likee> Likees { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vieww> Viewws { get; set; }

        public virtual PostCategory PostCategory { get; set; }
    }
}
