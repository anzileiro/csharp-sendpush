using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Csharp.SendPush.Model
{
    public sealed class Android : Device
    {
        public const string API_GCM = "https://android.googleapis.com/gcm/send";
        public const string API_KEY = "key=your-key";
        public const string API_ID = "id=your-id";

        public string CollapseKey { get; set; }
        public byte[] Push { get; set; }

        public Android(string deviceKey, string message, string collapseKey)
        {
            this.Key = deviceKey;
            this.Message = message;
            this.CollapseKey = collapseKey;
        }

        public async void Send()
        {
            var json = JObject.Parse("{'registration_ids':['" + this.Key + "'],'collapse_key':'" + this.CollapseKey + "','data':{'message':'" + this.Message + "'}}").ToString();

            Push = Encoding.UTF8.GetBytes(json);

            WebRequest webRequest = Request.Send(API_GCM, this.Push.Length, new Header[]
            {
                new Header 
                { 
                    Name = "Authorization",
                    Value = API_KEY
                },
                new Header 
                { 
                    Name = "Sender",
                    Value = API_ID
                }
            }, contentType: "application/json");

            Stream.Send(webRequest, this.Push);

            await Response.GetAsync(webRequest);
        }
    }
}