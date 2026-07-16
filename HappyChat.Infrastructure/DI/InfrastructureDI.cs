using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Repositories;
using HappyChat.Application.Contracts.Services;
using HappyChat.Infrastructure.Persistance.DbContext;
using HappyChat.Infrastructure.Persistance.Repositories;
using HappyChat.Infrastructure.Persistance.UnitOfWork;
using HappyChat.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HappyChat.Infrastructure.DI;

public static class InfrastructureDI
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HappyChat");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserChatRepository, UserChatRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Services
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IOTPService, OTPService>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        #endregion

        return services;
    }
}