using Disc_Golf_Scorecard;
using System;
using Xunit;

namespace Xunit.UWP.Tests
{
    public class DiscGolfTests
    {
        #region Stock Passing Test

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        private int Add(int firstNum, int secondNum)
        {
            return firstNum + secondNum;
        }

        #endregion

        #region Course Class Tests
        [Fact]
        public void Test_Course_Creation()
        {
            Course course = new Course("Big Rapids", 18);
            
            Assert.Equal("Big Rapids", course.Course_Name);
            
            Assert.NotNull(course);
        }

        [Fact]
        public void Test_Course_GenericParEqualsThree()
        {
            Course course = new Course("Big Rapids", 18);
            Test_Course_Creation();

            for (int i = 0; i < course.NumberOfHoles; i++)
            {
                Assert.Equal(3, course.CurrentHolePar[i]);
            }

        }

        [Fact]
        public void Test_Course_IncrementParOfCurrentHole()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18);//New courses automatically start at par 3 for every hole

            course.IncrementHolePar(currentHole);
            Assert.Equal(4, course.CurrentHolePar[currentHole]);

            course.IncrementHolePar(currentHole);
            Assert.Equal(5, course.CurrentHolePar[currentHole]);
        }

        [Fact]
        public void Test_Course_DecrementParOfCurrentHole()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18); //New courses automatically start at par 3 for every hole

            course.DecrementHolePar(currentHole);
            Assert.Equal(2, course.CurrentHolePar[currentHole]);

            course.DecrementHolePar(currentHole);
            Assert.Equal(1, course.CurrentHolePar[currentHole]);
        }

        [Fact]
        public void Test_Course_ParOutOfBoundsBehaviour()
        {
            int currentHole = 2;
            Course course = new Course("Big Rapids", 18); //New courses automatically start at par 3 for every hole

            course.DecrementHolePar(currentHole);
            course.DecrementHolePar(currentHole);
            Assert.Equal(1, course.CurrentHolePar[currentHole]);

            //Shouldn't actually decrement below 1
            course.DecrementHolePar(currentHole);
            Assert.Equal(1, course.CurrentHolePar[currentHole]);
        }

        [Fact]
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
            Assert.True(exception != null);
        }

        [Fact]
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
            Assert.True(exception != null);
        }

        [Fact]
        public void Test_Course_ChangeCourseName_GoodInput()
        {
            Course course = new Course("Big Rapids", 18);
            course.Course_Name = "Newago";

            Assert.Equal("Newago", course.Course_Name);

        }

        [Fact]
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

            Assert.True(exception != null);
        }

#endregion

        #region Player Class Tests

        [Fact]
        public void Test_Player_Creation_GoodInput()
        {
            Course course = new Course("Big Rapids", 18);

            Player player = new Player("Mike", course);

            Assert.Equal("Mike", player.Name);

        }

        [Fact]
        public void Test_Player_Creation_BadName()
        {
            Course course = new Course("Big Rapids", 18);

            Exception exception = Record.Exception(()=> { Player player = new Player("M@ke", course); });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Test_Player_Creation_BadCourse()
        {
            Course course = null;

            Exception exception = Record.Exception(() => { Player player = new Player("Mike", course); });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Test_Player_Score_InitializedWithPars()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);

            //Act
            Player player = new Player("Mike", course);

            //Assert
            for (int i = 0; i < course.NumberOfHoles; i++)
            {
                Assert.Equal(course.CurrentHolePar[i], player.Score[i]);
            }

        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]
        public void Test_Player_Score_IncrementScore(int value)
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player player = new Player("Mike", course);
            int currentHole = value;
           
            //Act
            player.IncrementCurrentScore(currentHole);
            int expectedValue = course.CurrentHolePar[currentHole] + 1;

            //Assert
            Assert.Equal(expectedValue, player.Score[currentHole]);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]
        public void Test_Player_Score_DecrementScore(int value)
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player player = new Player("Mike", course);
            int currentHole = value;

            //Act
            player.DecrementCurrentScore(currentHole);
            int expectedValue = course.CurrentHolePar[currentHole] - 1;

            //Assert
            Assert.Equal(expectedValue, player.Score[currentHole]);

        }

        #endregion

        #region Scorecard Class Tests

        [Fact]
        public void Test_Scorecard_Initialize()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };

            ScoreCard scorecard = new ScoreCard(players, course);
        }

        [Fact]
        public void Test_Scorecard_GetCurrentHole()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };

            //Act  Should initialize to hole 1 standard.
            ScoreCard scorecard = new ScoreCard(players, course);

            //Assert
            Assert.Equal(1, scorecard.CurrentHole);
        }

        [Fact]
        public void Test_Scorecard_GoToNextHole()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };
            ScoreCard scorecard = new ScoreCard(players, course);

            //Act  
            scorecard.NextHole();

            //Assert
            Assert.Equal(2, scorecard.CurrentHole);
        }

        [Fact]
        public void Test_Scorecard_GoToPreviousHole()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };
            ScoreCard scorecard = new ScoreCard(players, course);
            scorecard.NextHole();
            scorecard.NextHole();
            Assert.Equal(3, scorecard.CurrentHole);

            //Act  
            scorecard.PreviousHole();

            //Assert
            Assert.Equal(2, scorecard.CurrentHole);
        }

        [Fact]
        public void Test_Scorecard_CurrentHole_TestingLowerLimit()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };
            ScoreCard scorecard = new ScoreCard(players, course);

            //Act
            scorecard.PreviousHole();
            
            //Assert
            Assert.Equal(1, scorecard.CurrentHole);
            
        }

        [Fact]
        public void Test_Scorecard_CurrentHole_TestingUpperLimit()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };
            ScoreCard scorecard = new ScoreCard(players, course);

            //Act
            for (int i = 0; i < course.NumberOfHoles+2; i++)
            {
                scorecard.NextHole();
            }

            //Assert
            Assert.Equal(course.NumberOfHoles, scorecard.CurrentHole);
        }

        [Fact]
        public void Test_Scorecard_SelectPlayer()
        {
            //Arrange
            Course course = new Course("Big Rapids", 18);
            Player mike = new Player("Mike", course);
            Player andrew = new Player("Andrew", course);
            Player[] players = new Player[2] { mike, andrew };
            ScoreCard scorecard = new ScoreCard(players, course);

            //Act
            
            //Assert
            //scorecard.Player[0].scoreIncrement
        }




        #endregion


    }
}