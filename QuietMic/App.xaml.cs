using System.Windows;

namespace QuietMic
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Exit(object sender, ExitEventArgs exitEventArgs)
        {
            Dispatcher.Invoke(() =>
            {
                var window = QuietMic.MainWindow.Instance;
                var device = window.CurrentMic.Device;
                if (CheckBoxUtil.IsTwoDimChecked(window.EnableMicOnQuit) && device.IsMuted)
                {
                    device.ToggleMute();
                }
            });
        }
    }
}
