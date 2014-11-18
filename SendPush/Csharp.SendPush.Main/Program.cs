using Csharp.SendPush.Model;

namespace Csharp.SendPush.Main
{
    public class Program
    {
        public const string WP_DEVICE_KEY_TEST = "YOUR_DEVICE_JEY";

        public static void Main(string[] args)
        {
            var wp = new WindowsPhone(WP_DEVICE_KEY_TEST, "your message !!!", "yourViewInApp.xaml", TypeShipping.DeliverImmediate);
            wp.Send();
        }
    }
}
