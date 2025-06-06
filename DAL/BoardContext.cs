using Microsoft.EntityFrameworkCore;
using DAL;

namespace DAL
{
    public class BoardContext: DbContext
    {
        public BoardContext(DbContextOptions<BoardContext> options) : base(options)
        {
           
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const int nameLength = 32;
            const int passLength = 64;
            const int descLength = 256;

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Username);

                entity.Property(u => u.Username)
                      .HasMaxLength(nameLength)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .HasMaxLength(passLength)
                      .IsRequired();

                entity.HasMany(u => u.Announcements)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.Username)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(a => a.AnnouncementId);

                entity.Property(a => a.Title)
                      .HasMaxLength(nameLength);

                entity.Property(a => a.Description)
                      .HasMaxLength(descLength)
                      .IsRequired();

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Announcements)
                      .HasForeignKey(a => a.Username)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Category)
                      .WithMany(c => c.Announcements)
                      .HasForeignKey(a => a.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.CategoryName)
                     .HasMaxLength(nameLength);
                entity.Property(a => a.SubcategoryName)
                     .HasMaxLength(nameLength);

                entity.HasOne(a => a.Subcategory)
                      .WithMany(s => s.Announcements)
                      .HasForeignKey(a => a.SubcategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(a => a.Tags)
                      .WithMany(t => t.Announcements)
                      .UsingEntity(j => j.ToTable("AnnouncementTags"));
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);

                entity.Property(c => c.Name)
                      .HasMaxLength(nameLength)
                      .IsRequired();

             
                entity.HasOne(c => c.Heading)
                      .WithMany(h => h.Categories)
                      .HasForeignKey(c => c.HeadingId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.HasKey(s => s.SubcategoryId);

                entity.Property(s => s.Name)
                      .HasMaxLength(nameLength)
                      .IsRequired();

                entity.HasOne(s => s.Category)
                      .WithMany(c => c.Subcategories)
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Heading>(entity =>
            {
                entity.HasKey(h => h.HeadingId);

                entity.Property(h => h.Name)
                      .HasMaxLength(nameLength)
                      .IsRequired();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.TagId);

                entity.Property(t => t.Name)
                      .HasMaxLength(nameLength)
                      .IsRequired();
            });
        }



    }
}
