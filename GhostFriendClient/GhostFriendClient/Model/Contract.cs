using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class Contract
    {
        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        private CardSuit contractSuit;
        public CardSuit ContractSuit
        {
            get { return contractSuit; }
            set
            {
                contractSuit = value;
            }
        }

        public Contract(CardSuit contractSuit, int score)
        {
            this.contractSuit = contractSuit;
            this.score = score;
        }
    }
}
