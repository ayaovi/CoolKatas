using System;
using System.Linq;
using NUnit.Framework;

namespace TicTacToe.Tests
{
  [TestFixture]
  public class TicTacToeTests
  {
    [TestCase(-1, "Invalid Move")]
    [TestCase(9, "Invalid Move")]
    public void Play_GivenDefaultStateAndInvalidMove_ExpectInvalidMoveException(int move, string message)
    {
      //Arrange
      var currentState = new State();

      //Act
      var exception = Assert.Throws<ArgumentException>(() =>
       {
         var nextState = TicTacToe.Play(currentState, move, Field.X);
       });
      //Assert
      Assert.AreEqual(message, exception.Message);
    }

    [TestCase("...|...|...", 0, Field.X, "x..|...|...")]
    [TestCase("...|...|...", 7, Field.O, "...|...|.o.")]
    [TestCase("ox.|..o|..x", 3, Field.O, "ox.|o.o|..x")]
    public void Play_GivenStateMove_ExpectNextState(string strState, int move, Field tile, string expected)
    {
      //Arrange
      var currentState = new State(strState);

      //Act
      var nextState = TicTacToe.Play(currentState, move, tile);

      //Assert
      Assert.AreEqual(expected, nextState.ToString());
    }


    [Test]
    public void Play_GivenStateOneAndMoveOne_ExpectMoveAlreadyMadeError()
    {
      //Arrange
      var currentState = new State("x..|...|...");

      //Act
      var exception = Assert.Throws<ArgumentException>(() =>
      {
        var nextState = TicTacToe.Play(currentState, 0, Field.X);
      });

      //Assert
      Assert.AreEqual("Move Already Made", exception.Message);
    }


    [TestCase("...|...|...", false)]
    [TestCase("x..|.x.|..x", true)]
    [TestCase("..x|.xo|x.o", true)]
    [TestCase("xxx|.x.|...", true)]
    [TestCase("x.x|ooo|...", true)]
    [TestCase("x.x|...|xxx", true)]
    [TestCase("x.x|x..|x.x", true)]
    [TestCase(".xx|.x.|.x.", true)]
    [TestCase("..x|..x|..x", true)]
    public void IsWin_GivenDefaultState_ExpectResult(string strState, bool result)
    {
      //Arrange
      var state = new State(strState);

      //Act
      var isWin = TicTacToe.IsWin(state);

      //Assert
      Assert.AreEqual(result, isWin);
    }


    [TestCase("...|...|...", false)]
    [TestCase(".o.|ox.|..o", false)]
    [TestCase("xoo|oxx|oxo", true)]
    public void IsFull_GivenState_ExpectResult(string strState, bool result)
    {
      //Arrange
      var state = new State(strState);

      //Act
      var isFull = TicTacToe.IsFull(state);

      //Assert
      Assert.AreEqual(result, isFull);
    }


    [TestCase("...|...|...", true)]
    [TestCase("...|.x.|...", false)]
    public void IsEmpty_GivenState_ExpectResult(string strState, bool result)
    {
      //Arrange
      var state = new State(strState);

      //Act
      var isFull = TicTacToe.IsEmpty(state);

      //Assert
      Assert.AreEqual(result, isFull);
    }


    [TestCase("...|...|...", Field.X, 0)]
    [TestCase("...|.x.|...", Field.X, 4)]
    [TestCase("x..|...|...", Field.X, 3)]
    [TestCase("x..|.o.|...", Field.X, 2)]
    [TestCase("x..|o..|...", Field.X, 2)]
    [TestCase("xoo|.x.|xo.", Field.X, 5)]
    public void GetWinningPaths_GivenState_ExpectNumberOfPaths(string strState, Field tile, int paths)
    {
      //Arrange
      var state = new State(strState);

      //Act
      var winningPaths = TicTacToe.GetWinningPaths(state, tile);

      //Assert
      Assert.AreEqual(paths, winningPaths.Count);
    }


    [TestCase("xo.|oxx|oxo", 1)]
    [TestCase(".x.|...|...", 8)]
    [TestCase(".x.|xo.|..o", 5)]
    public void GetPossibleMoves_GivenState_ExpectNumberOfMoves(string strState, int moves)
    {
      //Arrange
      var state = new State(strState);

      //Act
      var possibleMoves = TicTacToe.GetPossibleMoves(state).ToArray();

      //Assert
      Assert.AreEqual(moves, possibleMoves.Length);
    }
  }
}