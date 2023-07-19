using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db.TaskQueue
{
    public interface IBackgroundTaskQueue
    {
        int Size { get; }

        void QueueBackgroundWorkItem(string message);

        Task<string> DequeueAsync(CancellationToken cancellationToken);
    }
}
