using Logic;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(buil =>
    {
        buil.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Content-Disposition")
        .AllowCredentials();
    });
});
#endif

builder.Services.AddDbContext<MyMoneyBContext>(opt =>
{
    opt.UseLazyLoadingProxies();
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("MB"),
        sql => sql.CommandTimeout(60 * 10));

});
builder.Services.AddDbContextFactory<MyMoneyBContext>(opt =>
{
    opt.UseLazyLoadingProxies();
    opt.UseSqlServer(
       builder.Configuration.GetConnectionString("MB"),
       sql => sql.CommandTimeout(60 * 10));
}, ServiceLifetime.Scoped);

#if !DEBUG
            builder.Services.AddOutputCache(x =>
            {
                x.AddPolicy("CachePostAndAuthenticated", OutputCachePolicy.Instance);
            });
#endif
builder.Services.AddScoped<EmailService>();
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IDBService, DBService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovingService, MovingService>();
builder.Services.AddScoped<IDebtsService, DebtsService>();
builder.Services.AddScoped<IReportsServies, ReportsService>();
builder.Services.AddScoped<IListService, ListService>();
builder.Services.AddScoped<IPresenceService, PresenceService>();
builder.Services.AddScoped<IPayOptionService, PayOptionService>();
builder.Services.AddScoped<IAreaServies, AreaServies>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IManagerDesignService, ManagerDesignService>();
builder.Services.AddScoped<IExpandedRevenuesService, ExpandedRevenuesSettingService>();
builder.Services.AddScoped<IPDFService, PDFService>();


builder.Services.AddSingleton<ITokenService, TokenService>();

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});


builder.Services.AddControllers(opt =>
{
    //  opt.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
    //   opt.ModelBinderProviders.Insert(0, new SanitizeBinder());
}).AddJsonOptions(options =>
{
    //  options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    // options.JsonSerializerOptions.Converters.Add(new StringConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

var app = builder.Build();

#if DEBUG
app.UseCors();
app.UseDeveloperExceptionPage();
#else
            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
#endif

app.UseAuthorization();
app.UseAuthentication();
#if !DEBUG
            app.UseOutputCache();
#endif

app.MapControllers();

app.Run();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}




