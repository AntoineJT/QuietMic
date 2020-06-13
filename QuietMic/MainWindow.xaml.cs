using System.Linq;
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

        private const int HomeKey = 0x24;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitializeMicList();

            var keyboardHookManager = KeyboardHookManagerSingleton.Instance;
            keyboardHookManager.Start();

            // let's use alt for now because for some reason control is not working
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.Alt, HomeKey, () =>
            {
                Dispatcher.Invoke(() =>
                {
                    CurrentMic.Device.ToggleMute();
                    RefreshToggleContent();
                });
            });
        }

        private void InitializeMicList()
        {
            var microphones = new CoreAudioController().GetCaptureDevices(DeviceState.Active).ToList();
            if (microphones.Count == 0)
            {
                Error.FatalErrorMessage("You don't have any active microphone! You can't use this application!", "No microphone found!");
            }

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
