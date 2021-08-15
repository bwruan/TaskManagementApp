using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectTask.Infrastructure.Repositories.Entities
{
    public partial class TaskManagementContext : DbContext
    {
        public TaskManagementContext()
        {
        }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-R3ND13SE\\SQLEXPRESS;Database=TaskManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaskDescription)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Taskee)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__TaskeeId__6DCC4D03");
            });

            modelBuilder.Entity<TaskComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__TaskComm__C3B4DFCAD3403796");

                entity.ToTable("TaskComment");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Commenter)
                    .WithMany(p => p.TaskComments)
                    .HasForeignKey(d => d.CommenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskComme__Comme__74794A92");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskComments)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskComme__TaskI__73852659");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
