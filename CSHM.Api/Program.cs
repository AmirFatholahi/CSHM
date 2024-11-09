using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Quartz;
using CSHM.Api.Extensions;
using CSHM.Api.IoC;
using CSHM.Api.Middlewares;
using CSHM.Core.Mapping;
using CSHM.Presentations.Login;
using CSHM.Data.Context;
using CSHM.Domain;
using Xceed.Document.NET;
using CSHM.Widget.Rest;
using System.Threading.RateLimiting;
using CSHM.Presentation.Base;
using CSHM.Widget.Captcha;
using CSHM.Api.Extensions;
using CSHM.Api.IoC;
using CSHM.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CSHMDBConnectionString"));
});

var corsAddressSection = builder.Configuration.GetSection("CorsAddress");

var corsAddresses = corsAddressSection.Get<List<CorsAddressViewModel>>();
var corsAddressArray = corsAddresses.Select(x => x.Address).ToArray();
if (corsAddresses.Any(x => x.Name == "All"))
{
    builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));
}
else
{

    builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins(corsAddressArray);
    }));
}

builder.Services.AddDefaultIdentity<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/CSHMSignalR")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };



        var validIssuersSection = builder.Configuration.GetSection("PublishedServerAddresses");
        var validIssuers = validIssuersSection.Get<List<ServerAddressViewModel>>().Select(x => x.Address).ToList();


        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudiences = validIssuers,
            ValidIssuers = validIssuers,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AFWAKHuioYAujasBkhd192388qwuyiHOriSS9EI90N9L08"))
        };

    }).AddCookie(options =>
    {
        options.LoginPath = "/";
        options.LogoutPath = "/";

    });

//builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

//builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
//   opt.TokenLifespan = TimeSpan.FromHours(1));

//builder.Services.AddIdentity<User, IdentityRole>(opt =>
//   {
//       opt.Password.RequiredLength = 7;
//       opt.Password.RequireDigit = false;
//       opt.Password.RequireUppercase = false;
//       opt.User.RequireUniqueEmail = true;
//   }).AddDefaultTokenProviders();

//services.Configure<IdentityOptions>(options =>
//{
//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;
//});
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

}
);

//========================================= /end of Identity 4.0

//======================================================================== Rate Limit Setting
var rateLimitSetting = builder.Configuration.GetSection("RateLimitSetting").Get<RateLimitViewModel>();


builder.Services.AddRateLimiter(options => {

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(partitionKey: httpContext.User.Identity?.Name ?? httpContext.Connection.RemoteIpAddress.ToString(), factory: partition => new SlidingWindowRateLimiterOptions
        {
            SegmentsPerWindow = rateLimitSetting.SegmentsPerWindow,
            AutoReplenishment = true,
            PermitLimit = rateLimitSetting.PermitLimit,
            QueueLimit = rateLimitSetting.QueueLimit,
            Window = TimeSpan.FromSeconds(rateLimitSetting.WindowSecond)
        }));


    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s).", cancellationToken: token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later.", cancellationToken: token);
        }
    };
});


//========================================= /end of Rate Limit

builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

//========================Auto Mapping
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MyMapper());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//===== /end of Mapping
builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                    UnicodeRanges.CjkUnifiedIdeographs }));

//===================/For  Change Razor Html Page 
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(DynamicAuthorizationFilter));
    options.Filters.Add(new DecoderFallbackExceptionFilter());
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
        options.InvalidModelStateResponseFactory = context =>
        {
            var problems = new CustomBadRequest(context);

            return new BadRequestObjectResult(problems);
        };
    })
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });


RegisterServices(builder.Services);


builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();


app.UseRateLimiter();




//app.UseStatusCodePagesWithRedirects("/Error/Error{0}");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Xss-Protection", "1");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    //context.Response.Headers.Add(
    //    "Content-Security-Policy",
    //    "script-src 'self' 'unsafe-inline'; " +
    //    "style-src 'self' 'unsafe-inline'; " +
    //    "img-src 'self' data:;");
    context.Response.Headers.Remove("Server");
    context.Request.Headers.Remove("Server");
    await next();
});


app.UseCors("MyPolicy");

app.UseStaticFiles();

app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();

//app.UseIdentityServer();
app.UseAuthorization();
app.UseErrorLogging();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//    endpoints.MapRazorPages();
//    //============================================================== HUB ================================
//    //endpoints.MapHub<ServiceHub>("/CSHMSignalR");
//    //==============================================================  /end of HUB ================================

//});

//new Aspose.Words.License().SetLicense($"{Directory.GetCurrentDirectory()}\\license\\Aspose.Total.NET.lic");

app.Run();

void RegisterServices(IServiceCollection services)
{
    Injector.RegisterServices(services);
}