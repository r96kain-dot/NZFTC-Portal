using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adds MVC controller and view support.
builder.Services.AddControllersWithViews();

// Adds in-memory storage for session data.
builder.Services.AddDistributedMemoryCache();

// Adds session support for lightweight login state.
builder.Services.AddSession(options =>
{
    // Keeps the session active for the sprint demo flow.
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    // Prevents JavaScript from reading the session cookie.
    options.Cookie.HttpOnly = true;

    // Marks the cookie as essential for the app to function.
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configures the production error pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enables session before controller actions run.
app.UseSession();

app.UseAuthorization();

// Sets the login page as the startup route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();