using System.Windows;
using System.Windows.Controls;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

namespace QuietMic
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MicDevice CurrentMic => (MicDevice) MicList.SelectedItem;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitializeMicList();
        }

        private void InitializeMicList()
        {
            var microphones = new CoreAudioController().GetCaptureDevices(DeviceState.Active);

            foreach (var mic in microphones)
            {
                var device = new MicDevice(mic);
                MicList.Items.Add(device);

                if (mic.IsDefaultDevice)
                {
                    MicList.SelectedItem = device;
                    RefreshToggleContent();
                }
            }

            MicList.IsReadOnly = true;
        }

        public void RefreshToggleContent()
        {
            Toggle.Content = CurrentMic.Device.IsMuted ? "Unmute" : "Mute";
        }

        private void MicList_Change(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            RefreshToggleContent();
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMic == null)
            {
                Error.FatalErrorMessage("Toggle_Click: CurrentMic should not be null");
                return;
            }
            CurrentMic.Device.ToggleMute();
            RefreshToggleContent();
        }
    }
}
