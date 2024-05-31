using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace course_marketplace.Models;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<CourseContent> CourseContents {get; set;}
    public DbSet<FileModel> FileModel {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the many-to-many relationship between User and Course
        modelBuilder.Entity<UserCourse>()
            .HasKey(uc => new { uc.UserId, uc.CourseId });

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCourses)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(uc => uc.CourseId);

        modelBuilder.Entity<Course>()
            .HasMany(ccc => ccc.CourseContents)
            .WithOne(cc => cc.Course)
            .HasForeignKey(ccc => ccc.CourseId);
        
        modelBuilder.Entity<CourseContent>()
            .HasMany(cc => cc.ContentFiles)
            .WithOne(fm => fm.CourseContent)
            .HasForeignKey(fm => fm.CourseContentId);


        var admin = new IdentityRole("admin");
        admin.NormalizedName = "Администратор";

        var customer = new IdentityRole("client");
        customer.NormalizedName = "Клиент";

        var seller = new IdentityRole("seller");
        seller.NormalizedName = "Продавец";

        modelBuilder.Entity<IdentityRole>().HasData(admin, customer, seller);
    }
}
