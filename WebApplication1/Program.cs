var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "best",
    pattern: "best/{postId?}");

app.MapControllerRoute(
    name: "stream_free",
    pattern: "stream_free/{postId?}");

app.MapControllerRoute(
    name: "stream_meme",
    pattern: "stream_meme/{postId?}");

app.Run();
