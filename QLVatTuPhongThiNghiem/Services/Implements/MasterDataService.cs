using QLVatTuPhongThiNghiem.Models.Entities;
using QLVatTuPhongThiNghiem.Repositories.Implements;
using QLVatTuPhongThiNghiem.Services.Interfaces;
using QLVatTuPhongThiNghiem.Data;

namespace QLVatTuPhongThiNghiem.Services.Implements
{
    public class MasterDataService : IMasterDataService
    {
        private readonly MasterDataRepository _masterDataRepository;
        private readonly AppDbContext _context;

        public MasterDataService(MasterDataRepository masterDataRepository, AppDbContext context)
        {
            _masterDataRepository = masterDataRepository;
            _context = context;
        }

        public async Task<IEnumerable<Loai>> GetLoaiAsync()
        {
            return await _masterDataRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ThuongHieu>> GetThuongHieuAsync()
        {
            return await _masterDataRepository.GetThuongHieuAsync();
        }

        public async Task<IEnumerable<PhongMay>> GetPhongMayAsync()
        {
            return await _masterDataRepository.GetPhongMayAsync();
        }

        public async Task<(bool Success, string Message)> CreateLoaiAsync(Loai loai)
        {
            try
            {
                var ketQua = await _masterDataRepository.CreateLoaiAsync(loai);

                switch (ketQua)
                {
                    case 1:
                        return (true, "Thêm loại thiết bị thành công");
                    case 0:
                        return (false, "Mã loại đã tồn tại");
                    case -1:
                        return (false, "Lỗi hệ thống");
                    default:
                        return (false, "Lỗi không xác định");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateLoaiAsync(Loai loai)
        {
            try
            {
                var ketQua = await _masterDataRepository.UpdateLoaiAsync(loai);

                switch (ketQua)
                {
                    case 1:
                        return (true, "Cập nhật loại thiết bị thành công");
                    case 0:
                        return (false, "Không tìm thấy loại thiết bị");
                    case -1:
                        return (false, "Lỗi hệ thống");
                    default:
                        return (false, "Lỗi không xác định");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteLoaiAsync(int maLoai)
        {
            try
            {
                var ketQua = await _masterDataRepository.DeleteLoaiAsync(maLoai);

                switch (ketQua)
                {
                    case 1:
                        return (true, "Xóa loại thiết bị thành công");
                    case 0:
                        return (false, "Không thể xóa do có thiết bị đang sử dụng loại này");
                    case -1:
                        return (false, "Lỗi hệ thống");
                    default:
                        return (false, "Lỗi không xác định");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }
}

