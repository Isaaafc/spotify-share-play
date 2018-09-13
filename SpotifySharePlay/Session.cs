using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SpotifySharePlay {
    /// <summary>
    /// Maintains a session's variables and communicates with the server
    /// </summary>
    public class Session {
        private const string domain = "http://localhost";

        public string CurrentTrackUri { get; set; }
        public int CurrentState { get; set; }
        public int Interval { get; set; }

        private string key;
        private DownloadManager downloadManager;

        public string Key {
            get {
                return key;
            }

            set {
                if (ValidateKey(value))
                    key = value;
                else
                    key = null;
            }
        }
        
        public Session() {
            downloadManager = new DownloadManager(domain);
        }

        /// <summary>
        /// Syncs the playback status with the server
        /// </summary>
        public void Sync() {
            if (PlaybackChanged()) {
                /// If the track is changed locally, send a signal to the server to update its state

            } else {
                /// If the user have not changed the track, get from the server the status of the track playback
                
            }
        }

        public bool PlaybackChanged() {
            return false;
        }

        public bool GenerateKey() {
            Key = "asdfoijwef";

            return Key != null;
        }

        private bool ValidateKey(string val) {
            return true;
        }
    }
}
