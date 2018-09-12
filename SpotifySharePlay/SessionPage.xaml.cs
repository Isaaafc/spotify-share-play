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
    /// Interaction logic for SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page {
        private SpotifyApiManager manager;
        private Session session;

        public SessionPage(SpotifyApiManager manager) {
            InitializeComponent();

            this.manager = manager;
            session = new Session();
        }

        private void btnCreateSession_Click(object sender, RoutedEventArgs e) {
            if (session.GenerateKey()) {
                tbSessionKey.Text = session.Key;
            }
        }

        private void btnJoinSession_Click(object sender, RoutedEventArgs e) {
            
        }
    }
}
