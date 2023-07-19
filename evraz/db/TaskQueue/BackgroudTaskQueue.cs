using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db.TaskQueue
{
    public class BackgroudTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<String> queue = new ConcurrentQueue<String>();
        private SemaphoreSlim signal = new SemaphoreSlim(0);
        public int Size => queue.Count;

        public async Task<String> DequeueAsync(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            queue.TryDequeue(out var item);

            return item;
        }

        public void QueueBackgroundWorkItem(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            queue.Enqueue(message);

            signal.Release();
        }
    }
}
