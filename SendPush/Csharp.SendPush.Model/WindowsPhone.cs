using System;
using System.Net;
using System.Text;

namespace Csharp.SendPush.Model
{
    public sealed class WindowsPhone : Device
    {
        public string Screen { get; set; }
        public byte[] Push { get; set; }

        public WindowsPhone(string deviceKey, string message, string screen, TypeShipping typeShipping)
        {
            this.Key = deviceKey;
            this.Message = message;
            this.Screen = screen;
            this.TypeShipping = Convert.ToString(typeShipping.GetHashCode());
        }

        public async void Send()
        {
            string toast = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<wp:Notification xmlns:wp=\"WPNotification\">" +
               "<wp:Toast>" +
                    "<wp:Text1>" + this.Message + "</wp:Text1>" +
                    "<wp:Param>" + this.Screen + "</wp:Param>" +
               "</wp:Toast> " +
            "</wp:Notification>";

            Push = Encoding.UTF8.GetBytes(toast);

            WebRequest webRequest = Request.Send(this.Key, this.Push.Length, new Header[]
            {
                new Header 
                { 
                    Name = "X-WindowsPhone-Target", 
                    Value = "toast"
                },
                new Header 
                { 
                    Name = "X-NotificationClass", 
                    Value = this.TypeShipping 
                }
            });

            Stream.Send(webRequest, this.Push);

            await Response.GetAsync(webRequest);
        }
    }
}
