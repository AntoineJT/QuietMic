using System.Windows;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

namespace QuietMic
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillMicList();
            var mic = (MicDevice) MicList.SelectedItem;
            RefreshToggleContent(mic);
        }

        private void FillMicList()
        {
            var microphones = new CoreAudioController().GetCaptureDevices(DeviceState.Active);

            foreach (var mic in microphones)
            {
                var device = new MicDevice(mic);
                MicList.Items.Add(device);

                if (mic.IsDefaultDevice)
                {
                    MicList.SelectedItem = device;
                }
            }

            MicList.IsReadOnly = true;
        }

        private void RefreshToggleContent(MicDevice mic)
        {
            Toggle.Content = mic.Device.IsMuted ? "Unmute" : "Mute";
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            var mic = (MicDevice) MicList.SelectedItem;
            if (mic == null)
            {
                Error.FatalErrorMessage("Toggle_Click: mic should not be null");
                return;
            }
            mic.Device.ToggleMute();
            RefreshToggleContent(mic);
        }

        private void Listen_Checked(object sender, RoutedEventArgs e)
        {
            var mic = (MicDevice) MicList.SelectedItem;

        }
    }
}
