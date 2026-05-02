using Backend.Backend.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUlid;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend.Backend
{
    public class DatabaseLibrary : IdentityDbContext<User>
    {
        public DatabaseLibrary(DbContextOptions<DatabaseLibrary> options) : base(options)
        {
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Program_> Programs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendance>()
                .Property(s => s.Status)
                .HasConversion<string>();

            /* 
            RolePermission is a Junction Table
            Role_ID and Permission_ID as Composite Key not Superkey
            It Asks both ID to locate uniqueness and considered an Primary Key

            eg. 
            Role : Permission
            SuperUser -> User.Update
            SuperUser -> User.Delete

            Both form uniqueness
            ps. (Worth it Ma'am Joan's Lessons HAHAHAHA)
            */
            modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.Role_ID, rp.Permission_ID });

            modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.Role_ID);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission_Entity)
                .WithMany()
                .HasForeignKey(rp => rp.Permission_ID);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                // check if new user
                if (entry.State == EntityState.Added &&
                    string.IsNullOrEmpty(entry.Entity.Id))
                {
                    entry.Entity.Id = NUlid.Ulid.NewUlid().ToString();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
