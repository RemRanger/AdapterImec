using AdapterImec.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdapterImec.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection));
            services.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped));

            services.AddScoped<ISerializionManager, SerializionManager>();
        }
    }
}
