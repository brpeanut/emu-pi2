﻿using emu_pi2.Core.Logging;
using emu_pi2.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using emu_pi2.UI.Extensions;
using emu_pi2.UI.Devices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace emu_pi2.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Splash : Page
    {
        private bool _loaded;

        public Splash()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += ClosePage;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Start the Animation.
            LoadIn.Completed += (o, args) =>
            {
                _loaded = true;
            };
            LoadIn.Begin();

            GamePad.Current.ButtonChanged += ClosePage;
            GamePad.Current.Start(MainViewModel.Current.Dispatcher);
        }
        
        private void ClosePage(object sender, object e)
        {
            if (_loaded)
            {
                _loaded = false;
                GamePad.Current.ButtonChanged -= ClosePage;
                GamePad.Current.Stop();
                Window.Current.CoreWindow.KeyDown -= ClosePage;
                MainViewModel.Current.LayoutRoot.NavigateToWithTransition(typeof(MainPage), LoadOut);
            }
        }
    }
}
