using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace DAL
{
    public partial class HotlineContext : DbContext
    {
        public HotlineContext() => Database.EnsureCreated();



        public HotlineContext(DbContextOptions<HotlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        //    }
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9O6G9O3\\SQLEXPRESS;Database=Hotline;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("DAL"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.Id)
                    .HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.HostId).HasColumnName("HostID");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HostID");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                modelBuilder.Entity<Game>(entity =>
                {
                    entity.ToTable("Game");

                    entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd(); // Используйте ValueGeneratedOnAdd для автоинкрементных столбцов

                    entity.Property(e => e.HostId).HasColumnName("HostID");

                    entity.HasOne(d => d.Host)
                        .WithMany(p => p.Games)
                        .HasForeignKey(d => d.HostId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("HostID");
                });

                modelBuilder.Entity<Player>(entity =>
                {
                    entity.ToTable("Player");

                    entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd(); // Используйте ValueGeneratedOnAdd для автоинкрементных столбцов

                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnType("character varying").HasMaxLength(50);

                    entity.Property(e => e.Password)
                        .IsRequired()
                        .HasColumnType("character varying").HasMaxLength(50);
                });

                modelBuilder.Entity<Session>(entity =>
                {
                    entity.ToTable("Session");

                    entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd(); // Используйте ValueGeneratedOnAdd для автоинкрементных столбцов

                    entity.Property(e => e.UserId).HasColumnName("UserID");

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Sessions)
                        .HasForeignKey(d => d.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PlayerID");
                });

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
