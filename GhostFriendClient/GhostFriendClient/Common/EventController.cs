using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GhostFriendClient.Common
{
    public class StringEventArgs : EventArgs
    {
        public String param { get; set; }
    }

    class EventController
    {
        private static EventController instance;
        public static EventController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventController();
                }

                return instance;
            }
        }

        public event EventHandler JoiningGameFailed;
        public void OnJoiningGameFailed(EventArgs e)
        {
            EventHandler handler = JoiningGameFailed;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> PlayerUpdated;
        public void OnPlayerUpdated(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = PlayerUpdated;
            handler?.Invoke(this, e);
        }
    }
}