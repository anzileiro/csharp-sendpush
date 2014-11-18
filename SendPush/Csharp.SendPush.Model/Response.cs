using System.Net;
using System.Threading.Tasks;

namespace Csharp.SendPush.Model
{
    public sealed class Response
    {
        public static async Task GetAsync(WebRequest webRequest)
        {
            using (WebResponse webResponse = await webRequest.GetResponseAsync())
            {
                string notificationStatus = webResponse.Headers["X-NotificationStatus"];
                string subscriptionStatus = webResponse.Headers["X-SubscriptionStatus"];
                string deviceConnectionStatus = webResponse.Headers["X-DeviceConnectionStatus"];
            }
        }
    }
}
