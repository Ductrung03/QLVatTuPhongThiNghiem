using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Data;
using QLVatTuPhongThiNghiem.Models.Entities;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;

namespace QLVatTuPhongThiNghiem.Repositories.Implements
{
    public class MasterDataRepository : IBaseRepository<Loai>
    {
        private readonly AppDbContext _context;

        public MasterDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loai>> GetAllAsync()
        {
            return await _context.Loai.ToListAsync();
        }

        public async Task<Loai> GetByIdAsync(int id)
        {
            return await _context.Loai.FindAsync(id);
        }

        public async Task<Loai> AddAsync(Loai entity)
        {
            await _context.Loai.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Loai> UpdateAsync(Loai entity)
        {
            _context.Loai.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Loai.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Specific methods for stored procedures
        public async Task<int> CreateLoaiAsync(Loai loai)
        {
            var ketQuaParam = new SqlParameter("@KetQua", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_Loai_Insert @MaLoai, @TenLoai, @KetQua OUTPUT",
                new SqlParameter("@MaLoai", loai.MaLoai),
                new SqlParameter("@TenLoai", loai.TenLoai),
                ketQuaParam
            );

            return (int)ketQuaParam.Value;
        }

        public async Task<int> UpdateLoaiAsync(Loai loai)
        {
            var ketQuaParam = new SqlParameter("@KetQua", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_Loai_Update @MaLoai, @TenLoai, @KetQua OUTPUT",
                new SqlParameter("@MaLoai", loai.MaLoai),
                new SqlParameter("@TenLoai", loai.TenLoai),
                ketQuaParam
            );

            return (int)ketQuaParam.Value;
        }

        public async Task<int> DeleteLoaiAsync(int maLoai)
        {
            var ketQuaParam = new SqlParameter("@KetQua", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_Loai_Delete @MaLoai, @KetQua OUTPUT",
                new SqlParameter("@MaLoai", maLoai),
                ketQuaParam
            );

            return (int)ketQuaParam.Value;
        }

        public async Task<IEnumerable<ThuongHieu>> GetThuongHieuAsync()
        {
            return await _context.Database.SqlQueryRaw<ThuongHieu>(
                "EXEC SP_ThuongHieu_GetAll"
            ).ToListAsync();
        }

        public async Task<IEnumerable<PhongMay>> GetPhongMayAsync()
        {
            return await _context.PhongMay.ToListAsync();
        }
    }
}