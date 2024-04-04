using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Utility.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();



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
        // 403 에러 
        OnForbidden = context =>
        {
            return new JwtEventsHandler().OnForbidden(context);
        },
        // 401 에러 발생시
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

//builder.Services.AddDbContext<CopycatContext>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("DefaultConnectionString")));
//builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<CopycatContext>();
//builder.Services.AddScoped<RoleManager<Role>>();
//builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
//{
//    options.Conventions.AuthorizeAreaFolder("Identity", "/Account");
//    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Shared/Login");
//});

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseMiddleware<AddHeaderMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "home",
    pattern: "");
//app.MapRazorPages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();

app.Run();