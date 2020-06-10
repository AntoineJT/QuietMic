using AudioSwitcher.AudioApi.CoreAudio;

namespace QuietMic
{
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