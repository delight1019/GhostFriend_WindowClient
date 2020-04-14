using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class GameParams
    {
        public static char DATA_DELIMITER = '/';
        public static char COMMAND_DELIMITER = '-';
        public static char COMMAND_DATA_DELIMITER = '_';

        // Send Parameters
        public static string COMPLETE_REQUEST = "CompleteRequest";
        public static string JOIN_GAME = "JoinGame";
        public static string ASK_PLAYERS_INFO = "AskPlayersInfo";
        public static string JOIN_NEW_PLAYER = "JoinNewPlayer";
        public static string EXIT_PLAYER = "ExitPlayer";
        public static string ALL_PLAYERS_ENTERED = "AllPlayersJoin";
        public static string REPLY_DEAL_MISS = "ReplyDealMiss";        

        // Receive Parameters
        public static string JOIN_SUCCESS = "JoinSuccess";
        public static string JOIN_FAIL = "JoinFail";
        public static string DISTRIBUTE_CARDS = "DistributesCard";
        public static string CHECK_DEAL_MISS = "CheckDealMiss";
        public static string RESTART_GAME = "RestartGame";
        public static string ASK_GIRU = "AskGiru";
    }
}
