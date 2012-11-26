using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using At.Phone.Control.Prompt;

namespace OKr.MXReader.Client.Core
{
    public class OkrToast
    {
        public static void Show(string message)
        {
            ToastPrompt prompt = new ToastPrompt();

            prompt.Message = message;
            prompt.Show();
        }

        public static void Show(string message, Action completed)
        {
            Show("消息提示", message, completed);
        }

        public static void Show(string title, string message, Action completed)
        {
            ToastPrompt toast = new ToastPrompt
            {
                Title = title,
                Message = message,
                FontSize = 20,
                TextOrientation = System.Windows.Controls.Orientation.Vertical,
                IsTimerEnabled = true,
                MillisecondsUntilHidden = 2000
            };
            toast.Completed += (obj, ea) =>
            {
                if (completed != null)
                {
                    completed();
                }
            };

            toast.Show();
        }

        public static void ServerError()
        {
            Show("服务连接错误。");
        }
    }
}
