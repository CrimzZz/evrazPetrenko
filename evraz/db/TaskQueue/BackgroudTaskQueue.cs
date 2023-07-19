using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db.TaskQueue
{
    internal class BackgroudTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<String> queue = new ConcurrentQueue<String>();
        private SemaphoreSlim signal = new SemaphoreSlim(0);
        public int Size => queue.Count;

        public async string DequeueAsync(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            queue.TryDequeue(out var item);

            return item;
        }

        public void QueueBackgroundWorkItem(string message)
        {
            throw new NotImplementedException();
        }
    }
}
