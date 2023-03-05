using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Interfaces;
using MyTestProject.Workers;

namespace MyTestProject.Register
{
    public static class FileRegister
    {
        public static void AddFileWorker(this IServiceCollection services) 
        {
            services.AddSingleton<IDataReader, DataReader>();
            services.AddSingleton<IDataWriter, DataWriter>();
            services.AddSingleton<FileWorker>();
        }
    }
}
