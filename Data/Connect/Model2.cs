namespace Data.Connect
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Target)
                .IsUnicode(false);
        }
    }
}
