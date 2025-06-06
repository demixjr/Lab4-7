using AutoMapper;
using BLL.interfaces;
using BLL;
using Ninject;
using BLL.services;
using Microsoft.Extensions.Configuration;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace PL
{
    public static class DIContainer
    {
        public static IServiceCollection CreateServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BoardContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, BoardContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IValidation, Validation>();

            services.AddAutoMapper(typeof(MappingProfile), typeof(MappingProfilePL));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IHeadingService, HeadingService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubcategoryService, SubcategoryService>();
            services.AddTransient<IAnnouncementService, AnnouncementService>();
            services.AddTransient<IServiceFacade, ServiceFacade>();

            services.AddTransient<ITokenService>(provider => new TokenService(configuration));

            return services;
        }
    }
}