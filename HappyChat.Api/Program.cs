using HappyChat.Application.Contracts.Services;
using HappyChat.Application.DI;
using HappyChat.Infrastructure.DI;
using HappyChat.Infrastructure.Persistance.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Business.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SysAdmin", policy => policy.RequireRole("SysAdmin"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});
#endregion

builder.Services.AddServices(builder.Configuration);
builder.Services.AddApplication();
//builder.Services.AddValidators(builder.Configuration);
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

    var seeder = new InitialDataSeeder(context, passwordService);

    await seeder.SeedDataAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
