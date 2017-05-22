using System;
using System.Text;

namespace TicTacToe
{
  class Runner
  {
    private static void Main()
    {
      var state = new State();

      var ais = new[]
      {
        new Gamer {Indicator = Field.X, Name = "AI 1", GameState = state, Oponent = Field.O},
        new Gamer {Indicator = Field.O, Name = "AI 2", GameState = state, Oponent = Field.X}
      };

      var currentPlayer = new Random().Next(2);
      var message = $"{ais[currentPlayer].Name} turn: ";

      while (!TicTacToe.IsWin(state) && !TicTacToe.IsFull(state))
      {
        Console.Clear();
        DisplayBoard(state);
        Console.Write(message);

        ais[currentPlayer].History.Add(new Play());
        var move = ais[currentPlayer].MakeMove();
        Console.WriteLine(move);
        Console.ReadLine();

        try
        {
          state = TicTacToe.Play(state, move, ais[currentPlayer].Indicator);
          ais[currentPlayer].History
          ais[0].GameState = state;
          ais[1].GameState = state;

          currentPlayer = currentPlayer == 0 ? 1 : 0;
          message = $"{ais[currentPlayer].Name} Turn: ";
        }
        catch (Exception e)
        {
          if (e.Message == "Invalid Move")
          {
            message = "Invalid Move, Please try again: ";
            continue;
          }
          if (e.Message == "Move Already Made")
          {
            message = $"Cell {move} is not empty, Please choose another: ";
          }
        }
      }

      Console.Clear();
      DisplayBoard(state);

      if (!TicTacToe.IsFull(state))
      {
        Console.WriteLine(currentPlayer == 0 ? "AI 2 Won." : "AI 1 Won.");
      }
      else
      {
        Console.WriteLine("It is a Tie");
      }
    }

    private static void DisplayBoard(State state)
    {
      var board = new StringBuilder();

      board.Append(" --- --- --- \n");

      var strState = state.ToString().Split('|');

      foreach (var row in strState)
      {
        board.Append("| ");
        foreach (var s in row)
        {
          board.Append(s + " | ");
        }
        board.Append("\n --- --- --- \n");
      }

      Console.WriteLine(board.ToString());
    }
  }
}