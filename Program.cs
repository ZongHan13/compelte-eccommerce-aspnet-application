using eTicket.Data;
using eTicket.Data.Cart;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IProducerService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddScoped<IOrderService, OrdersService>();
//Authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();


builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Movies}/{action=Index}/{id?}");
//Seed database
AppDbInitialize.Seed(app);
AppDbInitialize.SeedUsersAndRolesAsync(app).Wait();
app.Run();
