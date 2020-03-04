using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class GameParams
    {
        // Send Parameters
        public static string JOIN_GAME = "JoinGame";
        public static string ASK_PLAYERS_INFO = "AskPlayersInfo";

        // Receive Parameters
        public static string JOIN_SUCCESS = "JoinSuccess";
        public static string JOIN_FAIL = "JoinFail";        
    }
}
