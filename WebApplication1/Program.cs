using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        //ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey"))),
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            return new JwtEventsHandler().OnChallenge(context);
        },
        OnMessageReceived = context =>
        {
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var user = context.Principal.Identity.Name;
            //Grab the http context user and validate the things you need to
            //if you are not satisfied with the validation fail the request using the below commented code
            //context.Fail("Unauthorized");

            //otherwise succeed the request
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseMiddleware<AddHeaderMiddleware>();
//app.UseStatusCodePagesWithRedirects("/login");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "home",
    pattern: "");

app.MapControllerRoute(
    name: "best",
    pattern: "best/{postId?}");

app.MapControllerRoute(
    name: "stream_free",
    pattern: "stream_free/{postId?}");

app.MapControllerRoute(
    name: "stream_meme",
    pattern: "stream_meme/{postId?}");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();