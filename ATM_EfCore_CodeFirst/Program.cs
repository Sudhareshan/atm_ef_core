using ATM_EfCore_CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Bank_CustDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection")));
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ATM/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ATM}/{action=Index}/{id?}");
//// Create a scope for dependency injection
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<Bank_CustDbContext>();

//    // Apply migrations to ensure the database schema is up-to-date
//    dbContext.Database.Migrate();

//    // Create your trigger using the method you've defined
//    dbContext.CreateTrigger();
//}

app.Run();
