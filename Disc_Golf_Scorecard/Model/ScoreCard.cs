using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Golf_Scorecard
{
    public class ScoreCard
    {

        #region Private fields

        private Player[] players;
        private static int currentHole;
        private Course course;

        #endregion

        #region Constructors

        public ScoreCard(Player[] players, Course course)
        {
            this.players = players;
            this.course = course;
            currentHole = 1;
        }

        #endregion

        #region Public Properties

        public int CurrentHole
        {
            get { return currentHole; }
        }

        public Player[] Players
        {
            get { return players; }
            set { players = value; }
        }

        public int NumberOfPlayers
        {
            get { return players.Length; }
        }

        #endregion

        #region Public Methods

        public void NextHole()
        {
            if (currentHole < course.NumberOfHoles)
            {
                currentHole++;
            }
        }

        public void PreviousHole()
        {
            if (currentHole > 1)
            {
                currentHole--;
            }
        }
        #endregion

        #region Private Helper methods



        #endregion
    }
}
