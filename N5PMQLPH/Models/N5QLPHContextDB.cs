using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace N5PMQLPH.Models
{
    public partial class N5QLPHContextDB : DbContext
    {
        public N5QLPHContextDB()
            : base("name=N5QLPHContextDB22")
        {
        }

        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public virtual DbSet<PhanCong> PhanCongs { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<TaiKhoanUD> TaiKhoanUDs { get; set; }
        public virtual DbSet<ThietBi> ThietBis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GiangVien>()
                .HasMany(e => e.PhanCongs)
                .WithRequired(e => e.GiangVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiPhong>()
                .HasMany(e => e.Phongs)
                .WithRequired(e => e.LoaiPhong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.PhanCongs)
                .WithRequired(e => e.Phong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.ThietBis)
                .WithRequired(e => e.Phong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoanUD>()
                .Property(e => e.ID)
                .IsFixedLength();
        }
    }
}
