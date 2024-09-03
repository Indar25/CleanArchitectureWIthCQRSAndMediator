using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWIthCQRSAndMediator.Infrastructure.Data
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext()
        {
            
        }
        public BlogDBContext(DbContextOptions<BlogDBContext> options) :
            base(options)
        {

        }
        public virtual DbSet<Blog> Blogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Blog__3214EC07DFD4F4D5");

                entity.ToTable("Blog");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           // OnModelCreatingPartial(modelBuilder);
        }

      //  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
