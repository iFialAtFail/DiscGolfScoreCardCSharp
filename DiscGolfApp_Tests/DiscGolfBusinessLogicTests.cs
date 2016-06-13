using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Disc_Golf_Scorecard;

namespace DiscGolfApp_Tests
{
    [TestClass]
    public class DiscGolfBusinessLogicTests
    {

        //[TestMethod]
        //public void Test_Initialization()
        //{
        //    ScoreCard sc = new ScoreCard(2);
        //}
        #region Course Class Tests

        [TestMethod]
        public void Test_Course_Creation()
        {
            Course course = new Course("Big Rapids", 18);
            Assert.AreEqual("Big Rapids", course.Course_Name);
            Assert.IsNotNull(course);
        }

        [TestMethod]
        public void Test_Course_GenericParEqualsThree()
        {
            Course course = new Course("Big Rapids", 18);
            Test_Course_Creation();

            for (int i = 0; i < course.NumberOfHoles; i++)
            {
                Assert.AreEqual(3, course.CurrentHolePar[i]);
            }

        }

        [TestMethod]
        public void Test_Course_IncrementParOfCurrentHole()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18);//New courses automatically start at par 3 for every hole

            course.IncrementHolePar(currentHole);
            Assert.AreEqual(4, course.CurrentHolePar[currentHole]);

            course.IncrementHolePar(currentHole);
            Assert.AreEqual(5, course.CurrentHolePar[currentHole]);
        }

        [TestMethod]
        public void Test_Course_DecrementParOfCurrentHole()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18); //New courses automatically start at par 3 for every hole

            course.DecrementHolePar(currentHole);
            Assert.AreEqual(2, course.CurrentHolePar[currentHole]);

            course.DecrementHolePar(currentHole);
            Assert.AreEqual(1, course.CurrentHolePar[currentHole]);
        }

        [TestMethod]
        public void Test_Course_ParOutOfBoundsBehaviour()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18); //New courses automatically start at par 3 for every hole

            course.DecrementHolePar(currentHole);
            course.DecrementHolePar(currentHole);
            Assert.AreEqual(1, course.CurrentHolePar[currentHole]);

            //Shouldn't actually decrement below 1
            course.DecrementHolePar(currentHole);
            Assert.AreEqual(1, course.CurrentHolePar[currentHole]);
        }

        [TestMethod]
        public void Test_Course_CourseName_BadInput()
        {
            Exception exception = null;
            try
            {
                Course newCourse = new Course("Big R@pids", 18);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsTrue(exception != null);
        }

        [TestMethod]
        public void Test_Course_Constructor_badNumberOfHoles()
        {
            Exception exception = null;
            try
            {
                Course newCourse = new Course("Big Rapids", 0);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsTrue(exception != null);
        }

        [TestMethod]
        public void Test_Course_ChangeCourseName_GoodInput()
        {
            Course course = new Course("Big Rapids", 18);
            course.Course_Name = "Newago";

            Assert.AreEqual("Newago", course.Course_Name);

        }

        [TestMethod]
        public void Test_Course_ChangeCourseName_BadInput()
        {
            Exception exception = null;

            Course course = new Course("Big Rapids", 18);
            try
            {
                course.Course_Name = "N@wago";
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsTrue(exception != null);
        }

        #endregion

        #region Player Class Tests

        [TestMethod]
        public void Test_Player_Creation()
        {
            Course course = new Course("Big Rapids", 18);

            Player player = new Player("Mike", course);

            Assert.AreEqual("Mike", player.Name);

        }

        #endregion

    }
}
