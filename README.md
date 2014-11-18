csharp-sendpush
===============

Send push notifications natively for iOS, Android and Windows Phone 8, developed in C #.

Example
===============

```csharp
var wp = new WindowsPhone("your_device_key", "Your Message !!!", "yourViewInApp.xaml", TypeShipping.DeliverImmediate); 
wp.Send();
```



TypeShipping, is time that the notification will be delivered in seconds.

DeliverImmediate = will be delivered as fast as possible, preferably now.

DeliverInSevenMinutes = will be delivered in 7 minutes.

DeliverInFifteenMinutes = will be delivered in 15 minutes.

yourViewInApp.xaml = Is the screen that opens when the user receives and tap the notification.


License
===============

Licensed under the <a href="http://opensource.org/licenses/mit-license.php">MIT License.</a>
