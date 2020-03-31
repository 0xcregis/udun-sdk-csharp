using System.Collections.Generic;

namespace Udun.Core.Http.Base
{
    public class HttpProvider : IHttpProvider
    {

        public HttpResponseParameter Excute(HttpRequestParameter requestParameter, string contentType, string accept)
        {
            if (string.IsNullOrEmpty(contentType))
                contentType = "application/x-www-form-urlencoded";
            if (string.IsNullOrEmpty(accept))
                accept = "application/json;charset=UTF-8";
            return HttpUtil.Excute(requestParameter, contentType, accept);
        }

        public static HttpResponseParameter HttpExcute(HttpRequestParameter requestParameter, string contentType, string accept)
        {
            if (string.IsNullOrEmpty(contentType))
                contentType = "application/x-www-form-urlencoded";
            if (string.IsNullOrEmpty(accept))
                accept = "application/json;charset=UTF-8";
            return HttpUtil.Excute(requestParameter, contentType, accept);
        }

        public static string HttpPostRaw(string url, string data)
        {
            return HttpUtil.Excute(url, data);
          
        }
    }
}
