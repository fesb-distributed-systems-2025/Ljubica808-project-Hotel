using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            // =====================
            // Repositories
            // =====================
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            // =====================
            // Services
            // =====================
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReservationService, ReservationService>();

            // =====================
            // Unit Of Work
            // =====================
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
