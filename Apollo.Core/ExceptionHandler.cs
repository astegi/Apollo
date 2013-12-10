using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.GUI;
using System.Diagnostics;

namespace Apollo.Core
{
    public class ExceptionHandler
    {
        /// <summary>
        /// Handles exceptions
        /// </summary>
        /// <param name="ex"></param>
        public static void Handle(Exception ex)
        {
            ErrorWindow window = ErrorWindow.HandleException(ex);
            window.ShowDialog();
        }
    }
}
