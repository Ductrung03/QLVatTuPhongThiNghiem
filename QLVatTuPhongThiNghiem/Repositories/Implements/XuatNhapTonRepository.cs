using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Data;
using QLVatTuPhongThiNghiem.Models.ViewModels;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;

namespace QLVatTuPhongThiNghiem.Repositories.Implements
{
    public class XuatNhapTonRepository : IXuatNhapTonRepository
    {
        private readonly AppDbContext _context;

        public XuatNhapTonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> XuatThietBiAsync(XuatNhapTonViewModel model)
        {
            var ketQuaParam = new SqlParameter("@KetQua", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_XuatThietBi @MaTTB, @SoLuong, @NguoiTao, @GhiChu, @KetQua OUTPUT",
                new SqlParameter("@MaTTB", model.MaTTB),
                new SqlParameter("@SoLuong", model.SoLuong),
                new SqlParameter("@NguoiTao", model.NguoiTao),
                new SqlParameter("@GhiChu", model.GhiChu ?? (object)DBNull.Value),
                ketQuaParam
            );

            return (int)ketQuaParam.Value;
        }

        public async Task<int> NhapThietBiAsync(XuatNhapTonViewModel model)
        {
            var ketQuaParam = new SqlParameter("@KetQua", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_NhapThietBi @MaTTB, @SoLuong, @NguoiTao, @GhiChu, @KetQua OUTPUT",
                new SqlParameter("@MaTTB", model.MaTTB),
                new SqlParameter("@SoLuong", model.SoLuong),
                new SqlParameter("@NguoiTao", model.NguoiTao),
                new SqlParameter("@GhiChu", model.GhiChu ?? (object)DBNull.Value),
                ketQuaParam
            );

            return (int)ketQuaParam.Value;
        }

        public async Task<IEnumerable<dynamic>> BaoCaoTonKhoAsync(int? maPhongMay = null)
        {
            var result = await _context.Database.SqlQueryRaw<dynamic>(
                "EXEC SP_BaoCaoTonKho @MaPhongMay",
                new SqlParameter("@MaPhongMay", maPhongMay ?? (object)DBNull.Value)
            ).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<XuatNhapTonViewModel>> GetAllAsync()
        {
            var query = @"
                SELECT x.MaPhieu, x.LoaiPhieu, x.MaTTB, x.SoLuong, x.NgayTao, x.NguoiTao, x.GhiChu,
                       CONCAT(l.TenLoai, ' - ', th.TenThuongHieu) as TenThietBi,
                       n.TenDangNhap as TenNguoiTao
                FROM XuatNhapTon x
                INNER JOIN TrangTB t ON x.MaTTB = t.MaTTB
                INNER JOIN Loai l ON t.MaLoai = l.MaLoai
                INNER JOIN ThuongHieu th ON t.MaThuongHieu = th.MaThuongHieu
                LEFT JOIN NguoiDung n ON x.NguoiTao = n.MaNguoiDung
                ORDER BY x.NgayTao DESC";

            return await _context.Database.SqlQueryRaw<XuatNhapTonViewModel>(query).ToListAsync();
        }
    }
}
