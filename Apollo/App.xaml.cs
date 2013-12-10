using System;
using System.Windows;
using Apollo.Core;

namespace Apollo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Proxy.TraceManager.LogMessage(5001, String.Empty);
            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        ~App()
        {
            Proxy.TraceManager.LogMessage(5002, String.Empty);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Proxy.TraceManager.LogException(e.ExceptionObject as Exception);
        }
    }
}
