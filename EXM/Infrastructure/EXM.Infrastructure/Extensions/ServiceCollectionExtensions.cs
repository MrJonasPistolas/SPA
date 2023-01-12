using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Serialization.Serializers;
using EXM.Base.Interfaces.Services.Storage;
using EXM.Base.Interfaces.Services.Storage.Provider;
using EXM.Base.Serialization.JsonConverters;
using EXM.Base.Serialization.Options;
using EXM.Base.Serialization.Serializers;
using EXM.Infrastructure.Repositories;
using EXM.Infrastructure.Services.Storage;
using EXM.Infrastructure.Services.Storage.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EXM.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient<IIncomeRepository, IncomeRepository>()
                .AddTransient<IIncomeCategoryRepository, IncomeCategoryRepository>()
                .AddTransient<IExpenseRepository, ExpenseRepository>()
                .AddTransient<IExpenseCategoryRepository, ExpenseCategoryRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}
