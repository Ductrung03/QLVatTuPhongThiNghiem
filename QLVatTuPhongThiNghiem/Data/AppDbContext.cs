using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Models.Entities;

namespace QLVatTuPhongThiNghiem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<Loai> Loai { get; set; }
        public DbSet<ThuongHieu> ThuongHieu { get; set; }
        public DbSet<PhongMay> PhongMay { get; set; }
        public DbSet<TrangTB> TrangTB { get; set; }
        public DbSet<LichThucHanh> LichThucHanh { get; set; }
        public DbSet<LichSuSuaChua> LichSuSuaChua { get; set; }
        public DbSet<DanhGiaCapDo> DanhGiaCapDo { get; set; }
        public DbSet<XuatNhapTon> XuatNhapTon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names to match database
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<Loai>().ToTable("Loai");
            modelBuilder.Entity<ThuongHieu>().ToTable("ThuongHieu");
            modelBuilder.Entity<PhongMay>().ToTable("PhongMay");
            modelBuilder.Entity<TrangTB>().ToTable("TrangTB");
            modelBuilder.Entity<LichThucHanh>().ToTable("LichThucHanh");
            modelBuilder.Entity<LichSuSuaChua>().ToTable("LichSuSuaChua");
            modelBuilder.Entity<DanhGiaCapDo>().ToTable("DanhGiaCapDo");
            modelBuilder.Entity<XuatNhapTon>().ToTable("XuatNhapTon");

            // Configure decimal precision
            modelBuilder.Entity<LichSuSuaChua>()
                .Property(e => e.ChiPhi)
                .HasPrecision(18, 2);

            // Configure relationships
            modelBuilder.Entity<TrangTB>()
                .HasOne(t => t.PhongMay)
                .WithMany()
                .HasForeignKey(t => t.MaPhongMay)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrangTB>()
                .HasOne(t => t.Loai)
                .WithMany()
                .HasForeignKey(t => t.MaLoai)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrangTB>()
                .HasOne(t => t.ThuongHieu)
                .WithMany()
                .HasForeignKey(t => t.MaThuongHieu)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichSuSuaChua>()
                .HasOne(l => l.TrangTB)
                .WithMany()
                .HasForeignKey(l => l.MaTTB)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichSuSuaChua>()
                .HasOne(l => l.NguoiThucHien_User)
                .WithMany()
                .HasForeignKey(l => l.NguoiThucHien)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGiaCapDo>()
                .HasOne(d => d.TrangTB)
                .WithMany()
                .HasForeignKey(d => d.MaTTB)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGiaCapDo>()
                .HasOne(d => d.NguoiDanhGia_User)
                .WithMany()
                .HasForeignKey(d => d.NguoiDanhGia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<XuatNhapTon>()
                .HasOne(x => x.TrangTB)
                .WithMany()
                .HasForeignKey(x => x.MaTTB)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<XuatNhapTon>()
                .HasOne(x => x.NguoiTao_User)
                .WithMany()
                .HasForeignKey(x => x.NguoiTao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichThucHanh>()
                .HasOne(l => l.NguoiDung)
                .WithMany()
                .HasForeignKey(l => l.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
