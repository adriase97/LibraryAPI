using Core.Repositories;
using Core.Services;
using Infrastructure.Mappings;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the database context
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAuthorRepository), typeof(AuthorRepository));
            services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
            services.AddScoped(typeof(IPublisherRepository), typeof(PublisherRepository));
            services.AddScoped(typeof(IBookPublisherRepository), typeof(BookPublisherRepository));

            // Register Services
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookPublisherService, BookPublisherService>();

            // Register Mapping
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
