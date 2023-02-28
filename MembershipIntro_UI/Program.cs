
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using MembershipIntro_BusinessLayer.EmailSenderBusiness;
using MembershipIntro_BusinessLayer.Implementations;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_DataAccessLayer.ContextInfo;
using MembershipIntro_DataAccessLayer.ImplementationsOfRepo;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_EntityLayer.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Context bilgisini ekleyelim
builder.Services.AddDbContext<MembershipIntroContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

//CookieAuthentication ayarý eklendi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); // Razor runtime buraya eklensin

//servislerin DI yaþam döngülerine göre ayarlarýný yapalým

builder.Services.AddScoped<IMemberRepository,MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
builder.Services.AddScoped<IPhoneTypeService, PhoneTypeService>();

builder.Services.AddScoped<IMemberPhoneRepository, MemberPhoneRepository>();
builder.Services.AddScoped<IMemberPhoneService, MemberPhoneService>();

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();


//Mapleme ayarý
builder.Services.AddAutoMapper(x =>
{
    //<Expression>Func<TModel,bool>>  ve 
    //< Expression > Func<TViewModel, bool> > filterlarý birbirine dönüþtümek için AutoMapper'ýn Expression mapleme paketine ihtiyacýmýz var 
    x.AddExpressionMapping();
    x.AddProfile(typeof(Maps)); //Kimin kime dönüþebileceðini Maps classý aracýlýðýyla belirledik. Bunu da ayarlara burada tanýmlýyoruz
});
// Session ekleyelim
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // wwwroot klasörü çalýþmaz
app.UseSession(); // oturum 
app.UseRouting(); // browserdaki url kýsmýndaki /controller/action

app.UseAuthentication(); //login ve logout iþlemleri için
app.UseAuthorization(); // [Authorize] attribute'nün çalýþmasý için gereklidir

//app.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:Management}/{controller=Admin}/{action=Dashboard}/{id?}"
//    );
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "myarea",
    pattern: "{area:exists}/{controller=Admin}/{action=Dashboard}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // application RUN eden komut.
