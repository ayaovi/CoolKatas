using System;
using System.Text;

namespace TicTacToe
{
  class Runner
  {
    private static void Main()
    {

      var ais = new[]
      {
        new Gamer {Indicator = Field.X, Name = "AI 1", Oponent = Field.O},
        new Gamer {Indicator = Field.O, Name = "AI 2", Oponent = Field.X}
      };

      var i = 0;

      while (true)
      {
        ais[0].History.Add(new Play());
        ais[1].History.Add(new Play());
        var state = new State();
        var currentPlayer = new Random().Next(2);
        var message = $"{ais[currentPlayer].Name} turn: ";

        while (!TicTacToe.IsWin(state) && !TicTacToe.IsFull(state))
        {
          Console.Clear();
          DisplayBoard(state);
          Console.Write(message);

          ais[0].GameState = state;
          ais[1].GameState = state;
          var move = ais[currentPlayer].MakeMove();
          Console.WriteLine(move);
          Console.ReadLine();

          try
          {
            state = TicTacToe.Play(state, move, ais[currentPlayer].Indicator);
            ais[currentPlayer].History[ais[currentPlayer].History.Count - 1]
                              .Turns.Add(new Turn{ GameState = ais[currentPlayer].GameState, Move = move });

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
          if (currentPlayer == 0)
          {
            ais[0].History.RemoveAt(ais[0].History.Count - 1);
            ais[1].History[ais[1].History.Count - 1].Outcome = 1;
          }
          else
          {
            ais[0].History[ais[0].History.Count - 1].Outcome = 1;
            ais[1].History.RemoveAt(ais[1].History.Count - 1);
          }
          Console.WriteLine(currentPlayer == 0 ? "AI 2 Won." : "AI 1 Won.");
        }
        else
        {
          Console.WriteLine("It is a Tie");
        }

        // Console.WriteLine($"{ais[0].Name} has {ais[0].History.Count} history data.");
        // Console.WriteLine($"{ais[1].Name} has {ais[1].History.Count} history data.");
        Console.ReadLine();
        ++i;
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