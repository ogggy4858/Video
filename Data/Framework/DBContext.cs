namespace Data.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryHistory> CategoryHistories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentNew> CommentNews { get; set; }
        public virtual DbSet<DisLike> DisLikes { get; set; }
        public virtual DbSet<DislikeNew> DislikeNews { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Likee> Likees { get; set; }
        public virtual DbSet<LikeNew> LikeNews { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Search> Searches { get; set; }
        public virtual DbSet<Vieww> Viewws { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryHistory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Image2)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.DislikeNews)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.LikeNews)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Offer>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Immage)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CommentNews)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.DisLikes)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PeopleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.DislikeNews)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PeopleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Histories)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Likees)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PeopleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.LikeNews)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PeopleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.News)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Replies)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Searches)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Viewws)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.PeopleID);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.DisLikes)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Likees)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Search>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Vieww>()
                .Property(e => e.IP)
                .IsUnicode(false);
        }
    }
}
