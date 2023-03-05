using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyTestProject.Interfaces
{
    public interface IDataWriter
    {
        Task WriteToFile(string path, IEnumerable<string> data, CancellationToken token = default);
    }
}
