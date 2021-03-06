﻿using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpotifySharePlay {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        SpotifyApiManager manager;
        SessionPage sessionPage;

        public MainWindow() {
            InitializeComponent();

            manager = new SpotifyApiManager();
            sessionPage = new SessionPage(manager);

            manager.AuthCompleted = () => {
                Dispatcher.Invoke(() => {
                    frame.Navigate(sessionPage);
                });
            };

            manager.Init();
        }

    }
}
