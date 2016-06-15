using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Disc_Golf_Scorecard
{
    public class Course
    {
        #region Private Fields

        private int[] holes;
        private string courseName;

        #endregion

        #region Constructors

        public Course(string courseName, int numHoles)
        {
            if (isValidCoursename(courseName))
            {
                this.courseName = courseName;
            }
            else
            {
                throw new ArgumentException("Invalid input for course name. AlphaNumeric characters and spaces only.");
            }

            if (numHoles > 0)
            {
                holes = new int[numHoles];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Amount of holes per course must be greater than one.");
            }

            initializeCoursePars();
        }





        #endregion

        #region Public Properties

        public string Course_Name
        {
            get { return courseName; }
            set
            {
                if (isValidCoursename(value))
                {
                    courseName = value;
                }

                else { throw new ArgumentException("Invalid input for course name. AlphaNumeric characters and spaces only."); }
                
            }
        }

        public int NumberOfHoles
        {
            get { return holes.Length; }
        }

        public int[] CurrentHolePar
        {
            get { return holes; }
        }

        #endregion

        #region Private Helper Methods

        private void initializeCoursePars()
        {
            for (int i = 0; i < holes.Length; i++)
            {
                holes[i] = 3;
            }
        }

        private bool isValidCoursename(string nameInput)
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

        #region Public Methods

        public void IncrementHolePar(int currentHole)
        {
            holes[currentHole]++;
        }

        public void DecrementHolePar(int currentHole)
        {
            if (holes[currentHole] <= 1)
            {
                //Do nothing, this is the bottom limit
            }
            else
            {
                holes[currentHole]--;
            }

        }

        #endregion

    }
}
