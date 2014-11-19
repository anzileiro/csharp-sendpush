using Csharp.SendPush.Model;

namespace Csharp.SendPush.Main
{
    public class Program
    {
        public const string WINDOWS_PHONE_DEVICE_KEY_TEST = "YOUR_DEVICE_KEY";
        public const string ANDROID_DEVICE_KEY_TEST = "YOUR_DEVICE_KEY";

        public static void Main(string[] args)
        {
            var windowsPhone = new WindowsPhone(WINDOWS_PHONE_DEVICE_KEY_TEST, "your Message !!!", "yourViewInApp.xaml", TypeShipping.DeliverImmediate);
            windowsPhone.Send();

            var android = new Android(ANDROID_DEVICE_KEY_TEST, "your Message !!!", "yourCollapseKey");
            android.Send();
        }
    }
}
