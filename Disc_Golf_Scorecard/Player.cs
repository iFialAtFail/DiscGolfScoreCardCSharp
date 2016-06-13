using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Golf_Scorecard
{
    public class Player
    {
        private string playerName;
        private int[] score;

        public Player(string playerName, Course course)
        {
            this.playerName = playerName;
            score = new int[course.NumberOfHoles];
        }

        public string Name
        {
            get { return playerName; }
            set { playerName = value; }
        }
    }
}
