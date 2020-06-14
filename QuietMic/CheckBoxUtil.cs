using System.Windows.Controls;

namespace QuietMic
{
    public static class CheckBoxUtil
    {
        public static bool IsTwoDimChecked(CheckBox checkBox)
        {
            return checkBox.IsChecked != null && (bool) checkBox.IsChecked;
        }
    }
}