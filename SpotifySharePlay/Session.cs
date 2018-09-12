using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySharePlay {
    /// <summary>
    /// Maintains a session's variables and communicates with the server
    /// </summary>
    public class Session {
        private string key;
        public string CurrentTrackUri { get; set; }
        public int CurrentState { get; set; }

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
