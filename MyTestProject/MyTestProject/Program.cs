using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyTestProject.Exceptions;
using MyTestProject.FilePathOptions;
using MyTestProject.Options;
using MyTestProject.Register;
using MyTestProject.Workers;

namespace MyTestProject
{
    class Program
    {
        private static PathOptions paths = null;
        private static ShedulerOptions sheduler = null;
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static CancellationToken token = tokenSource.Token;

        static async Task Main(string[] args) 
        {
            IHostBuilder builder = new HostBuilder();
            builder.ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile("application.json", false, true).AddEnvironmentVariables();
                IConfigurationRoot configuration = config.Build();
                paths = configuration.GetSection("Paths").Get<PathOptions>();
                sheduler = configuration.GetSection("Sheduler").Get<ShedulerOptions>();
            })
            .ConfigureServices(services =>
            {
                services.AddFileWorker();
            }).ConfigureLogging(logging => 
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            
            var app = builder.Build();
            StartApp(app);
            StopApp();
        }

        private async static Task StartApp(IHost app)
        {
            try
            {
                await app.Services.GetRequiredService<FileWorker>().Run(paths, sheduler.Time, token).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PathReferenceNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void StopApp()
        {
            Console.WriteLine("Нажмите любую кнопку для завершения операции");
            Console.ReadLine(); // для запроса на отмену
            tokenSource.Cancel();
            Console.WriteLine("Ждите завершения операции");
            Console.ReadLine(); // ждем пока отмениться по запросу, смотря сколько времени в sheduler
        }
    }
}
