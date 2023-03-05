using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using MyTestProject.FilePathOptions;
using MyTestProject.Interfaces;

namespace MyTestProject.Workers
{
    public class FileWorker
    {
        public FileWorker(IDataReader reader, IDataWriter writer, ILogger<FileWorker> logger)
        {
            _reader = reader;
            _writer = writer;
            _logger = logger;
        }

        public async Task Run(PathOptions paths, DateTime interval = default, CancellationToken token = default)
        {    
            while (!token.IsCancellationRequested)
            {
                _logger.LogWarning($"{interval.Second} секунд, до записи. {DateTime.Now}");
                await Task.Delay(1000 * interval.Second);

                var data = await _reader.ReadFromFile(paths.PathToRead).ConfigureAwait(false);

                if (data != null)
                {
                    await _writer.WriteToFile(paths.PathToWrite, data).ConfigureAwait(false);
                }

                _logger.LogInformation($"Запись завершена в {DateTime.Now}");
            }

            token.ThrowIfCancellationRequested();
        }

        private readonly IDataReader _reader;
        private readonly IDataWriter _writer;
        private readonly ILogger<FileWorker> _logger;
    }
}
