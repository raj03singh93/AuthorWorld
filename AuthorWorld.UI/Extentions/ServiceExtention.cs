using AuthorWorld.Domain.Data;
using AuthorWorld.Service.Concrete;
using AuthorWorld.Service.Contract;
using AuthorWorld.Service.Mapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorWorld.UI.Extentions
{
    public static class ServiceExtention
    {
        public static void ConfigureProcessService(this IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            var mapperConfig = new MapperConfiguration(option => option.AddProfile(new MappingProfile()));
            services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

            //Connect to DB
            services.AddStackExchangeRedisCache(config => 
            {
                config.Configuration = configuration.GetSection("RedisCacheSettings:Configuration").Value;
                config.InstanceName = configuration.GetSection("RedisCacheSettings:InstanceName").Value; ;
            });
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultDBConnection"));
            });

            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
