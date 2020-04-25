﻿using System;
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

        // Parameters
        public static string NO_CONTRACT = "NoContract";

        // Send Commands
        public static string COMPLETE_REQUEST = "CompleteRequest";
        public static string JOIN_GAME = "JoinGame";
        public static string JOIN_NEW_PLAYER = "JoinNewPlayer";
        public static string EXIT_PLAYER = "ExitPlayer";
        public static string REPLY_DEAL_MISS = "ReplyDealMiss";
        public static string DECLARE_CONTRACT = "DeclareContract";
        public static string PASS_CONTRACT_DECLERATION = "PassContractDecleration";
        public static string DISCARD_CARD = "DiscardCard";

        // Receive Commands
        public static string JOIN_FAIL = "JoinFail";
        public static string DISTRIBUTE_CARDS = "DistributesCard";
        public static string CHECK_DEAL_MISS = "CheckDealMiss";
        public static string RESTART_GAME = "RestartGame";
        public static string ASK_CONTRACT = "AskContract";
        public static string OTHER_PLAYER_ASKING_CONTRACT = "OtherPlayerAskingContract";
        public static string CASTER_DECLARED = "CasterDeclared";
        public static string START_DECLARER_CARD_SELECTION = "StartDeclarerCardSelection";
        public static string SELECT_CARDS_TO_DISCARD = "SelectCardsToDiscard";
    }
}
