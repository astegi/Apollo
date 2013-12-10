using System;
using System.Windows;
using System.Windows.Interop;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Apollo.GUI
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public string ErrorMessage { get { return errorMessage.Text; } }
        public string ErrorStackTrace { get { return errorStackTrace.Text; } }

        public ErrorWindow(string errorMessage, string errorStackTrace)
        {
            InitializeComponent();
            
            wImage.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Error.Handle, new Int32Rect(0, 0, 32, 32), BitmapSizeOptions.FromEmptyOptions());
            this.errorMessage.Text = errorMessage;
            this.errorStackTrace.Text = errorStackTrace;
        }

        public static ErrorWindow HandleException(Exception ex)
        {
            ErrorWindow window = new ErrorWindow(ex.Message, ex.StackTrace);
            window.Title = ex.GetType().Name;
            return window;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
