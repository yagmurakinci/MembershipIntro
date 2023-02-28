
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

//CookieAuthentication ayar� eklendi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); // Razor runtime buraya eklensin

//servislerin DI ya�am d�ng�lerine g�re ayarlar�n� yapal�m

builder.Services.AddScoped<IMemberRepository,MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
builder.Services.AddScoped<IPhoneTypeService, PhoneTypeService>();

builder.Services.AddScoped<IMemberPhoneRepository, MemberPhoneRepository>();
builder.Services.AddScoped<IMemberPhoneService, MemberPhoneService>();

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();


//Mapleme ayar�
builder.Services.AddAutoMapper(x =>
{
    //<Expression>Func<TModel,bool>>  ve 
    //< Expression > Func<TViewModel, bool> > filterlar� birbirine d�n��t�mek i�in AutoMapper'�n Expression mapleme paketine ihtiyac�m�z var 
    x.AddExpressionMapping();
    x.AddProfile(typeof(Maps)); //Kimin kime d�n��ebilece�ini Maps class� arac�l���yla belirledik. Bunu da ayarlara burada tan�ml�yoruz
});
// Session ekleyelim
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // wwwroot klas�r� �al��maz
app.UseSession(); // oturum 
app.UseRouting(); // browserdaki url k�sm�ndaki /controller/action

app.UseAuthentication(); //login ve logout i�lemleri i�in
app.UseAuthorization(); // [Authorize] attribute'n�n �al��mas� i�in gereklidir

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
