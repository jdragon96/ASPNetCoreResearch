using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().
                Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<User>()
            //    .Property(e => e.Role).HasConversion(new EnumToNumberConverter<UserRoleType, int>())
            //    .Property(e => e.Id)
            //    .ValueGeneratedOnAdd()
            // Id는 자동 증가 (기본적으로 EF Core가 자동으로 Identity로 설정)
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();  // 자동 증가로 설정 (Identity로 처리됨)

            // Role은 Enum을 int로 변환
            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .HasConversion(new EnumToNumberConverter<UserRoleType, int>())
                .IsRequired();  // Role이 필수로 설정되어 있는 경우
        }
    }
}
