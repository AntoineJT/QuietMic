using System;
using System.Diagnostics;
using System.Windows;

namespace QuietMic
{
    public static class Error
    {
        private const string AppName = "QuietMic";

        private static void InternalErrorMessage(string message, string errorLabel, string title)
        {
            Debug.Assert(message != null);
            Debug.Assert(title != null);
            
            var errorTitle = (title.Length > 0) ? $"{errorLabel} {title}" : errorLabel;
            MessageBox.Show(message, $"[{AppName}] {errorTitle}", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ErrorMessage(string message, string title = "")
        {
            InternalErrorMessage(message, "Error!", title);
        }

        public static void FatalErrorMessage(string message, string title = "")
        {
            InternalErrorMessage(message, "Fatal error!", title);
            Environment.Exit(1);
        }
    }
}