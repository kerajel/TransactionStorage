using Autofac;
using TransactionStorage.Interface;

namespace TransactionStorage
{
    public class Program
    {
        public static void Main()
        {
            var iContainer = Registry.BuildIContainer();

            using var scope = iContainer.BeginLifetimeScope();
            var storageService = scope.Resolve<IStorageService>();
            storageService.RunService();
        }
    }
}