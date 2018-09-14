using System.Windows;
using System.Windows.Controls;
using SpotifyAPI.Web;
using System.Threading;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifySharePlay {
    /// <summary>
    /// Interaction logic for SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page {
        private SpotifyApiManager manager;
        private Session session;
        private Timer timer;

        public SessionPage(SpotifyApiManager manager) {
            InitializeComponent();

            this.manager = manager;
            session = new Session();

            tbMsg.Text = "Click 'Create New Session' to create a new session / paste the key of an existing session, then click 'Join Session' to start";
        }

        /// <summary>
        /// Creates a thread to listen to playback changes
        /// </summary>
        private void StartSession() {
            timer = new Timer((obj) => {
                /// Sync
                session.Playback = manager.Spotify.GetPlayback();
                session.Sync();
                Console.WriteLine("PlaybackChanged: {0}", session.PlaybackChanged);

                if (session.Playback != null)
                    Console.WriteLine("IsPlaying: {0}", session.Playback.IsPlaying);

                if (session.PlaybackChanged) {
                    manager.Update(session.Playback);
                }
            }, null, 0, session.Interval);
        }

        private void EndSession() {
            if (timer != null)
                timer.Dispose();
        }

        private void ToggleSessionControls(bool enable) {
            btnCreateSession.Visibility = enable ? Visibility.Visible : Visibility.Collapsed;
            btnJoinSession.Visibility = enable ? Visibility.Visible : Visibility.Collapsed;

            txtSessionKey.IsReadOnly = !enable;

            btnQuitSession.Visibility = enable ? Visibility.Collapsed : Visibility.Visible;
            spPlayback.Visibility = enable ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btnCreateSession_Click(object sender, RoutedEventArgs e) {
            if (session.GenerateKey()) {
                txtSessionKey.Text = session.Key;
            }
        }

        private void btnJoinSession_Click(object sender, RoutedEventArgs e) {
            if ((session.Key = txtSessionKey.Text) != null) {
                ToggleSessionControls(false);

                StartSession();
            }
        }

        private void btnQuitSession_Click(object sender, RoutedEventArgs e) {
            ToggleSessionControls(true);
            EndSession();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e) {
            var playback = session.Playback;
            playback.IsPlaying = true;

            session.jsonResponse = JsonConvert.SerializeObject(playback);
        }

        private void btnPause_Click(object sender, RoutedEventArgs e) {
            var playback = manager.Spotify.GetPlayback();
            playback.IsPlaying = false;

            session.jsonResponse = JsonConvert.SerializeObject(playback);
        }
    }
}
