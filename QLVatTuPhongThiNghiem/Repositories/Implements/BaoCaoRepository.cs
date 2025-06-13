using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Data;
using QLVatTuPhongThiNghiem.Models.ViewModels;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;

namespace QLVatTuPhongThiNghiem.Repositories.Implements
{
    public class BaoCaoRepository : IBaoCaoRepository
    {
        private readonly AppDbContext _context;

        public BaoCaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ThongKeTheoPhongViewModel>> ThongKeThietBiTheoPhongAsync()
        {
            return await _context.Database.SqlQueryRaw<ThongKeTheoPhongViewModel>(
                "EXEC SP_ThongKeThietBiTheoPhong"
            ).ToListAsync();
        }

        public async Task<IEnumerable<ThongKeSuDungTheoThangViewModel>> ThongKeSuDungTheoThangAsync(int nam)
        {
            return await _context.Database.SqlQueryRaw<ThongKeSuDungTheoThangViewModel>(
                "EXEC SP_ThongKeSuDungTheoThang @Nam",
                new SqlParameter("@Nam", nam)
            ).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> BaoCaoChiPhiSuaChuaAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await _context.Database.SqlQueryRaw<dynamic>(
                "EXEC SP_BaoCaoChiPhiSuaChua @TuNgay, @DenNgay",
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            ).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> ThongKeDanhGiaCapDoAsync()
        {
            return await _context.Database.SqlQueryRaw<dynamic>(
                "EXEC SP_ThongKeDanhGiaCapDo"
            ).ToListAsync();
        }
    }
}

