using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Data;
using QLVatTuPhongThiNghiem.Models.ViewModels;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;

namespace QLVatTuPhongThiNghiem.Repositories.Implements
{
    public class ThongBaoRepository : IThongBaoRepository
    {
        private readonly AppDbContext _context;

        public ThongBaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(int KetQua, string ThongBao)> GuiThongBaoAsync(ThongBaoViewModel model)
        {
            try
            {
                var thongBao = new Models.Entities.ThongBao
                {
                    TieuDe = model.TieuDe,
                    NoiDung = model.NoiDung,
                    LoaiThongBao = model.LoaiThongBao,
                    MaNguoiGui = model.MaNguoiNhan,
                    MaNguoiNhan = model.MaNguoiNhan,
                    NgayTao = DateTime.Now,
                    DaDoc = false,
                    TrangThai = true
                };

                _context.ThongBao.Add(thongBao);
                await _context.SaveChangesAsync();

                return (1, "Gửi thông báo thành công");
            }
            catch (Exception ex)
            {
                return (-1, $"Lỗi: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ThongBaoViewModel>> GetThongBaoByNguoiDungAsync(int maNguoiDung)
        {
            var query = @"
                SELECT t.MaThongBao, t.TieuDe, t.NoiDung, t.LoaiThongBao, t.NgayTao, t.DaDoc,
                       ng.TenDangNhap + ' (' + ISNULL(ng.HoTen, ng.TenDangNhap) + ')' as TenNguoiGui,
                       CASE 
                           WHEN t.MaNguoiNhan IS NULL THEN N'Tất cả người dùng'
                           ELSE nn.TenDangNhap + ' (' + ISNULL(nn.HoTen, nn.TenDangNhap) + ')'
                       END as TenNguoiNhan
                FROM ThongBao t
                LEFT JOIN NguoiDung ng ON t.MaNguoiGui = ng.MaNguoiDung
                LEFT JOIN NguoiDung nn ON t.MaNguoiNhan = nn.MaNguoiDung
                WHERE t.TrangThai = 1
                ORDER BY t.NgayTao DESC";

            return await _context.ThongBaoViewModel.FromSqlRaw(query).ToListAsync();
        }

        public async Task<(int KetQua, string ThongBao)> DanhDauDaDocAsync(int maThongBao, int maNguoiDung)
        {
            try
            {
                var thongBao = await _context.ThongBao
                    .Where(t => t.MaThongBao == maThongBao &&
                               (t.MaNguoiNhan == maNguoiDung || t.MaNguoiNhan == null))
                    .FirstOrDefaultAsync();

                if (thongBao == null)
                {
                    return (0, "Không tìm thấy thông báo");
                }

                thongBao.DaDoc = true;
                await _context.SaveChangesAsync();

                return (1, "Đánh dấu đã đọc thành công");
            }
            catch (Exception ex)
            {
                return (-1, $"Lỗi: {ex.Message}");
            }
        }

        public async Task<int> GetSoThongBaoChuaDocAsync(int maNguoiDung)
        {
            return await _context.ThongBao
                .Where(t => (t.MaNguoiNhan == maNguoiDung || t.MaNguoiNhan == null) &&
                           !t.DaDoc && t.TrangThai)
                .CountAsync();
        }
    }
}
