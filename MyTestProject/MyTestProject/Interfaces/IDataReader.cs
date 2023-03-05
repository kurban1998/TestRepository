using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyTestProject.Interfaces
{
    public interface IDataReader
    {
        Task<IEnumerable<string>> ReadFromFile(string path, CancellationToken token = default);
    }
}
