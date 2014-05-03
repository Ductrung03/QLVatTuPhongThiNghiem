using QLVatTuPhongThiNghiem.Models.ViewModels;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;
using QLVatTuPhongThiNghiem.Services.Interfaces;

namespace QLVatTuPhongThiNghiem.Services.Implements
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly IThongBaoRepository _thongBaoRepository;

        public ThongBaoService(IThongBaoRepository thongBaoRepository)
        {
            _thongBaoRepository = thongBaoRepository;
        }

        public async Task<(bool Success, string Message)> GuiThongBaoAsync(ThongBaoViewModel model)
        {
            try
            {
                var (ketQua, thongBao) = await _thongBaoRepository.GuiThongBaoAsync(model);
                return (ketQua == 1, thongBao);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ThongBaoViewModel>> GetThongBaoByNguoiDungAsync(int maNguoiDung)
        {
            return await _thongBaoRepository.GetThongBaoByNguoiDungAsync(maNguoiDung);
        }

        public async Task<IEnumerable<ThongBaoViewModel>> GetAllThongBaoAsync()
        {
            return await _thongBaoRepository.GetAllThongBaoAsync();
        }

        public async Task<(bool Success, string Message)> DanhDauDaDocAsync(int maThongBao, int maNguoiDung)
        {
            try
            {
                var (ketQua, thongBao) = await _thongBaoRepository.DanhDauDaDocAsync(maThongBao, maNguoiDung);
                return (ketQua == 1, thongBao);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }

        public async Task<int> GetSoThongBaoChuaDocAsync(int maNguoiDung)
        {
            return await _thongBaoRepository.GetSoThongBaoChuaDocAsync(maNguoiDung);
        }
    }
}
