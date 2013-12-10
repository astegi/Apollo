using System.Windows;
using Apollo.Core;
using Apollo.Core.Miscellaneous;

namespace Apollo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Tracer.Log(5001, "Háhá, ez a helyi logban");
        }
    }
}
