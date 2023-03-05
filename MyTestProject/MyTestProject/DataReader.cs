using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MyTestProject.Exceptions;
using MyTestProject.Interfaces;

namespace MyTestProject
{
    public sealed class DataReader : IDataReader
    {
        public Task<IEnumerable<string>> ReadFromFile(string path, CancellationToken token)
        {
            if (!IsFileExistOrCreate(path)) 
            {
                throw new PathReferenceNotFoundException("Нет пути к файлу!");
            } 
            
            return Task.FromResult(File.ReadLines(path));
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
    }
}
