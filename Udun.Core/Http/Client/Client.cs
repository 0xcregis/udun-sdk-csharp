using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Http.Client
{
    public interface Client<T>
    {

        ResponseMessage<T> Post(string url, Dictionary<string, string> list);

        ResponseMessage<T> Get(string url);

    }
}
