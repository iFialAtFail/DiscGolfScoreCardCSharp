using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Golf_Scorecard
{
    public class Player
    {
        #region Private Fields

        private string playerName;
        private int[] score;

        #endregion

        #region Constructors

        public Player(string playerName, Course course)
        {
            if (isValidPlayerName(playerName))
            {
                this.playerName = playerName;
            }
            else
            {
                throw new ArgumentException("Invalid input for player name. AlphaNumeric characters and spaces only.");
            }

            if (course == null)
            {
                throw new ArgumentNullException();
            }

            score = (int[]) course.CurrentHolePar.Clone();
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int[] Score
        {
            get { return score; }
        }

        #endregion

        #region Public Methods

        public void IncrementCurrentScore(int currentHole)
        {
            score[currentHole]++;
        }

        public void DecrementCurrentScore(int currentHole)
        {
            score[currentHole]--;
        }

        #endregion

        #region Private Helper Methods

        private bool isValidPlayerName(string nameInput)
        {
            char[] charArray = nameInput.ToArray();


            foreach (char letter in charArray)
            {
                if (Char.IsLetterOrDigit(letter) || letter == ' ')
                {
                    //Let it keep iterating through the characters.
                }
                else { return false; }
            }
            return true;
        }


        #endregion

    }
}
