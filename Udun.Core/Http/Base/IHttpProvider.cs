using System.Collections.Generic;

namespace Udun.Core.Http.Base
{
    public interface IHttpProvider
    {
        HttpResponseParameter Excute(HttpRequestParameter requestParameter, string contentType, string accept);
    }
}
