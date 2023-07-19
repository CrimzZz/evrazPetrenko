using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db.Interfaces
{
    public interface IServiceSettings
    {

        public string Queue { get; }
        public string PublishTo { get; }
        public string Host { get; }
    }
}
