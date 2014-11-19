using System;
using System.Net;

namespace Csharp.SendPush.Model
{
    public sealed class Request
    {
        public static WebRequest Send(string requestUriString, long contentLength, Header[] headers, string method = "POST", string contentType = "text/xml")
        {
            WebRequest webRequest = WebRequest.Create(requestUriString);
            webRequest.Method = method;
            webRequest.ContentLength = contentLength;
            webRequest.ContentType = contentType;

            for (int i = 0; i < headers.Length; i++)
            {
                webRequest.Headers.Add(headers[i].Name, headers[i].Value);
            }

            return webRequest;
        }
    }
}
