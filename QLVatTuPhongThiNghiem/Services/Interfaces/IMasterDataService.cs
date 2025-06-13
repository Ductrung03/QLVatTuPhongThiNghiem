using QLVatTuPhongThiNghiem.Models.Entities;

namespace QLVatTuPhongThiNghiem.Services.Interfaces
{
    public interface IMasterDataService
    {
        Task<IEnumerable<Loai>> GetLoaiAsync();
        Task<IEnumerable<ThuongHieu>> GetThuongHieuAsync();
        Task<IEnumerable<PhongMay>> GetPhongMayAsync();
        Task<(bool Success, string Message)> CreateLoaiAsync(Loai loai);
        Task<(bool Success, string Message)> UpdateLoaiAsync(Loai loai);
        Task<(bool Success, string Message)> DeleteLoaiAsync(int maLoai);
    }
}