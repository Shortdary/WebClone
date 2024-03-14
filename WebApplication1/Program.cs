using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.Events = new JwtBearerEvents
//    {
//        OnMessageReceived = context =>
//        {
//            context.Token = context.Request.Cookies["Authentication"];
//            return Task.CompletedTask;
//        }
//    };
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            System.Diagnostics.Debug.WriteLine("OnMessageReceived");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            System.Diagnostics.Debug.WriteLine("OnTokenValidated");
            var user = context.Principal.Identity.Name;
            //Grab the http context user and validate the things you need to
            //if you are not satisfied with the validation fail the request using the below commented code
            //context.Fail("Unauthorized");

            //otherwise succeed the request
            return Task.CompletedTask;
        }
    };
    //options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey"))),
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256Signature }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


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

app.UseRouting();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5263"));

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();