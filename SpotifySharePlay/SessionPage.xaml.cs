using System.Windows;
using System.Windows.Controls;
using SpotifyAPI.Web;
using System.Threading;

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

            lblMsg.Content = "Click 'Create New Session' to create a new session / paste the key of an existing session, then click 'Join Session' to start";
        }

        /// <summary>
        /// Creates a thread to listen to playback changes
        /// </summary>
        private void StartSession() {
            timer = new Timer((obj) => {
                session.Sync();

                if (session.PlaybackChanged()) {
                    /// Update playback state in this instance
                    
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

            tbSessionKey.IsReadOnly = !enable;

            btnQuitSession.Visibility = enable ? Visibility.Visible : Visibility.Collapsed;
            spPlayback.Visibility = enable ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnCreateSession_Click(object sender, RoutedEventArgs e) {
            if (session.GenerateKey()) {
                tbSessionKey.Text = session.Key;
            }
        }

        private void btnJoinSession_Click(object sender, RoutedEventArgs e) {
            if ((session.Key = tbSessionKey.Text) != null) {
                ToggleSessionControls(false);

                StartSession();
            }
        }

        private void btnQuitSession_Click(object sender, RoutedEventArgs e) {
            ToggleSessionControls(true);
            EndSession();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e) {

        }

        private void btnPause_Click(object sender, RoutedEventArgs e) {

        }
    }
}
