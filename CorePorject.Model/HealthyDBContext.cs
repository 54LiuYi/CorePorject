using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CorePorject.Model
{
    public partial class HealthyDBContext : DbContext
    {
        public HealthyDBContext()
        {
        }

        public HealthyDBContext(DbContextOptions<HealthyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Archive> Archives { get; set; }
        public virtual DbSet<Authority> Authorities { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1;database=HealthyDB;uid=sa;pwd=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Archive>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.Property(e => e.No)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("NO");

                entity.Property(e => e.Allergy)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("过敏史");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Bmi)
                    .HasColumnName("BMI")
                    .HasComment("身体质量指数  体重÷身高²");

                entity.Property(e => e.Bust).HasComment("胸围");

                entity.Property(e => e.Chronic)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("慢性病");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dbp)
                    .HasColumnName("DBP")
                    .HasComment("舒张压  60-80mmHg");

                entity.Property(e => e.Education)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Family)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("家族史");

                entity.Property(e => e.Height).HasComment("身高");

                entity.Property(e => e.Hipline).HasComment("臀围");

                entity.Property(e => e.Hr)
                    .HasColumnName("HR")
                    .HasComment("心率 成年人为60-100次/分，3岁以下儿童多在100次/分以上，老年人稍慢");

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.Medical)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("病史");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Native)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("籍贯");

                entity.Property(e => e.Occupation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("工作");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Past)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("既往史");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((111))");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ri)
                    .HasColumnName("RI")
                    .HasComment("呼吸指数 正常值在1到30之间");

                entity.Property(e => e.Sbp)
                    .HasColumnName("SBP")
                    .HasComment("收缩压  100-120mmHg ");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SpO2).HasComment("血氧饱和度   90-100%");

                entity.Property(e => e.Temperature).HasComment("体温");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Waist).HasComment("腰围");

                entity.Property(e => e.Weight).HasComment("体重");
            });

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.ToTable("Authority");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasComment("菜单ID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasComment("排序");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新时间");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Authorities)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authority_Menus");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Authorities)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authority_Roles");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("图标");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("菜单名称");

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasComment("菜单父ID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新时间");

                entity.Property(e => e.ViewUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("视图地址");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("角色描述");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("角色名称");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("邮箱");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("img")
                    .HasComment("头像图片");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("账户");

                entity.Property(e => e.LoginPwd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("手机号码");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.State).HasComment("状态（0:正常，-1:冻结）");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新时间");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
