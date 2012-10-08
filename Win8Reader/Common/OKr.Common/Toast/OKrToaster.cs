using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common;
using OKr.Common.ToastContent;
using Windows.UI.Notifications;

namespace Sina.View.Common.Toast
{
    public class OKrToast
    {
        public static void Show(string title, string text)
        {
            IToastNotificationContent toastContent = null;

            IToastText03 templateContent = ToastContentFactory.CreateToastText03();
            templateContent.TextHeadingWrap.Text = title;
            templateContent.TextBody.Text = text;
            toastContent = templateContent;

            ToastNotification toast = toastContent.CreateNotification();
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(1);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public static void Show(string text)
        {
            IToastNotificationContent toastContent = null;

            IToastText01 templateContent = ToastContentFactory.CreateToastText01();
            templateContent.TextBodyWrap.Text = text;
            toastContent = templateContent;

            ToastNotification toast = toastContent.CreateNotification();
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(1);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
