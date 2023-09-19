using Microsoft.EntityFrameworkCore;
using Pokemon.DAL.Database;
using Pokemon.DAL.Helper;
using Pokemon.DAL.Repo;
using Pokemon.Model.IRpo;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Connection Server
var ConnectionString = builder.Configuration.GetConnectionString("ApplicationConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(ConnectionString));
#endregion

#region Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Auto Mapper
builder.Services.AddAutoMapper(option => option.AddProfile(new DomainProfile()));
#endregion

#region Cors
builder.Services.AddCors();
#endregion

#region Identity
/*builder.Services.AddIdentity<IdentityUser , IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequiredLength = 6;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
});
builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(3);
    option.AccessDeniedPath = "/Account/Login";
    option.LogoutPath = "/Account/Login";
    option.LoginPath = "/Account/Login";
});*/
#endregion

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Cors
app.UseCors(option => option
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);
#endregion

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
