﻿using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Csharp.SendPush.Model
{
    public enum TypeShipping
    {
        DeliverImmediate = 2,
        DeliverInSevenMinutes = 12,
        DeliverInFifteenMinutes = 900
    }

    public abstract class Device : Push
    {
        public string Key { get; set; }
    }

    public abstract class Push
    {
        public string Message { get; set; }
        public string TypeShipping { get; set; }
    }

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

    public sealed class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

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
