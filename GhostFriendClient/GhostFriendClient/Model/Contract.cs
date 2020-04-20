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
        private CardSuit suit;

        public Contract(CardSuit suit, int score)
        {
            this.suit = suit;
            this.score = score;
        }
    }
}
