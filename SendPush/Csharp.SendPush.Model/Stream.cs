using System.Net;

namespace Csharp.SendPush.Model
{
    public sealed class Stream
    {
        public static void Send(WebRequest webRequest, byte[] buffer)
        {
            using (System.IO.Stream stream = webRequest.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
