using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Viewer
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public static string commandFilename = null;
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length>0)
            {
                commandFilename = e.Args[0];
            }
        }
    }
}
