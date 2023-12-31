﻿using BookLook.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookLook.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
            services.AddTransient<BookParserService>();

            return services;
        }
    }
}