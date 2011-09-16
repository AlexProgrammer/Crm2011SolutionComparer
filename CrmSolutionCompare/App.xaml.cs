using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MvvmFoundation.Wpf;

// Alex.Net.Crm.SolutionCompare

namespace Alex.Net.Crm.SolutionCompare
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Messenger Messenger
        {
            get { return _messenger; }
        }

        readonly static Messenger _messenger = new Messenger();

    }
}
