using BookLook.Domain.Book;
using BookLook.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BookLook.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryAsync<Book>, InMemoryBookRepository>();

            return services;
        }
    }
}