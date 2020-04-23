using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class Player
    {
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
            }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public Player(int index, String name)
        {
            this.Index = index;
            this.Name = name;
        }
    }
}
