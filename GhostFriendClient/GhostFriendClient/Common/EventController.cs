using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GhostFriendClient.Common
{
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
    }
}