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
            MicDevice mic = MicList.SelectedItem as MicDevice;
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
            MicDevice mic = MicList.SelectedItem as MicDevice;
            mic.Device.ToggleMute();
            RefreshToggleContent(mic);
        }
    }

    // TODO Move it in a dedicated class
    public class MicDevice
    {
        public MicDevice(CoreAudioDevice device)
        {
            Device = device;
        }

        public CoreAudioDevice Device { get; }
        
        public override string ToString()
        {
            return Device.FullName;
        }
    }
}
