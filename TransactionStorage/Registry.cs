using Microsoft.Extensions.DependencyInjection;
using TransactionStorage.Core;
using TransactionStorage.Interface;
using TransactionStorage.Model;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text;
using TransactionStorage.InputProcessor;
using TransactionStorage.InputParser;

namespace TransactionStorage
{
    public static class Registry
    {
        public static IContainer BuildIContainer()
        {
            var services = new ServiceCollection();

            var builder = new ContainerBuilder();

            ConfigureLogging(services);
            ConfigureAppSettings(builder);

            builder.Populate(services);

            builder.RegisterType<StorageService>().As<IStorageService>().SingleInstance();
            builder.RegisterType<DbContext>().As<IDbContext<Transaction>>().SingleInstance();

            builder.RegisterType<GetProcessor>().As<IInputProcessor>();
            builder.RegisterType<AddProcessor>().As<IInputProcessor>();

            builder.RegisterType<InputValidator>().As<IInputValidator>();
            builder.RegisterType<InputProvider>().As<IInputProvider>();
            builder.RegisterType<InputEvaluator>().As<IInputEvaluator>();
            
            builder.RegisterType<IntegerParser>().As<IInputParser<int>>();
            builder.RegisterType<DateTimeParser>().As<IInputParser<DateTime>>();
            builder.RegisterType<DecimalParser>().As<IInputParser<decimal>>();

            builder.RegisterType<TransactionComparer>().As<IEqualityComparer<Transaction>>();
            builder.RegisterType<Serializer>().As<ISerializer>();

            return builder.Build();
        }

        private static void ConfigureLogging(ServiceCollection services)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(r =>
            {
                r.AddSerilog(logger);
            });
        }

        private static void ConfigureAppSettings(ContainerBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();

            builder.Register(_ => config.Get<AppSettings>()!).SingleInstance();
        }
    }
}