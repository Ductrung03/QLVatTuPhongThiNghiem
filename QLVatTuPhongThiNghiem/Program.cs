using Microsoft.EntityFrameworkCore;
using QLVatTuPhongThiNghiem.Data;
using QLVatTuPhongThiNghiem.Repositories.Interfaces;
using QLVatTuPhongThiNghiem.Repositories.Implements;
using QLVatTuPhongThiNghiem.Services.Interfaces;
using QLVatTuPhongThiNghiem.Services.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITrangTBRepository, TrangTBRepository>();
builder.Services.AddScoped<ILichThucHanhRepository, LichThucHanhRepository>();
builder.Services.AddScoped<ISuaChuaRepository, SuaChuaRepository>();
builder.Services.AddScoped<IDanhGiaCapDoRepository, DanhGiaCapDoRepository>();
builder.Services.AddScoped<IXuatNhapTonRepository, XuatNhapTonRepository>();
builder.Services.AddScoped<IBaoCaoRepository, BaoCaoRepository>();
builder.Services.AddScoped<MasterDataRepository>();

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITrangTBService, TrangTBService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<ILichThucHanhService, LichThucHanhService>();
builder.Services.AddScoped<ISuaChuaService, SuaChuaService>();
builder.Services.AddScoped<IXuatNhapTonService, XuatNhapTonService>();
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();
builder.Services.AddScoped<IDanhGiaCapDoService, DanhGiaCapDoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();