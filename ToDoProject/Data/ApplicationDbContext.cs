using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;


namespace ToDoProject.Models
{
    public class TaskMapping : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired().HasColumnName("Id");
            builder.Property(t => t.Name).IsRequired().HasColumnName("Name");
            builder.Property(t => t.Description).IsRequired().HasColumnName("Description");
        }
    }

    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired().HasColumnName("Id");
            builder.Property(t => t.Name).IsRequired().HasColumnName("Name");
            builder.Property(t => t.Age).IsRequired().HasColumnName("Age"); 
            builder.Property(t => t.Address).IsRequired().HasColumnName("Address");
            builder.Property(t => t.Position).IsRequired().HasColumnName("Position");
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<EmployeeTask> EmployeeTask { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Image> Image{ get; set; }

        private readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
             Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(logStream.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMapping());
            modelBuilder.ApplyConfiguration(new EmployeeMapping());

            modelBuilder.Entity<EmployeeTask>()
                .HasKey(t => new { t.EmployeeId, t.TaskId });

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(sc => sc.Employee)
                .WithMany(s => s.EmployeeTasks)
                .HasForeignKey(sc => sc.EmployeeId);

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(sc => sc.Tasks)
                .WithMany(c => c.EmployeeTasks)
                .HasForeignKey(sc => sc.TaskId);

            modelBuilder.Entity<Tasks>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Image)
                .WithOne(i => i.Employee)
                .HasForeignKey<Image>(e => e.EmployeeId)
                .IsRequired();
        }

        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
    }
}
