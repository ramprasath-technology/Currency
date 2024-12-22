using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkCalls.HTTP
{
    public interface IGetCalls
    { 
        Task<T?> Get<T>(string url);
    }
}
