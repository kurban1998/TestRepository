using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using MyTestProject.Exceptions;
using MyTestProject.Interfaces;

namespace MyTestProject
{
    public sealed class DataWriter : IDataWriter
    {
        public DataWriter(ILogger<DataWriter> logger)
        {
            _logger = logger;
        }

        public Task WriteToFile(string path, IEnumerable<string> data, CancellationToken token = default)
        {
            if (!IsFileExistOrCreate(path)) throw new PathReferenceNotFoundException("Нет пути к файлу!");
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                _logger.LogInformation("Файл открыт и идет запись...");
                sw.WriteLine($"Прочитано из файла в: {DateTime.Now}");

                foreach (var line in data)
                {
                    sw.WriteLine(line);
                }
                
                sw.Close();
                _logger.LogInformation("Запись окончена! Файл закрыт.");
            }

            return Task.CompletedTask;
        }

        private static bool IsFileExistOrCreate(string path)
        {
            if (path == null) return false;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                return true;
            }

            return true;
        }

        private readonly ILogger<DataWriter> _logger;
    }
}
