using System;
using System.Diagnostics;
using System.Windows;

namespace QuietMic
{
    public static class Error
    {
        public static void ErrorMessage(string message, string title = "")
        {
            Debug.Assert(message != null);
            MessageBox.Show(message, title.Length > 0 ? $"Error! {title}" : "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void FatalErrorMessage(string message, string title = "")
        {
            ErrorMessage(message, title);
            Environment.Exit(1);
        }
    }
}