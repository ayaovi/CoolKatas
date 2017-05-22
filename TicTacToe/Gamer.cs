using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
  public class Gamer
  {
    public Field Indicator { get; set; }
    public string Name { get; set; }
    public State GameState { get; set; }
    public Field Oponent { get; set; }
    public List<Play> History { get; set; } = new List<Play>();

    public int SelectOptimalMove()
    {
      var opponentWinningPaths = TicTacToe.GetWinningPaths(GameState, Oponent);

      if (opponentWinningPaths.Count != 0)
      {
        var oponentEminentWiningPaths = opponentWinningPaths.Where(path => path.Length == 1).ToList();
        var myWinningPaths = TicTacToe.GetWinningPaths(GameState, Indicator);
        var myEminentWinningPaths = myWinningPaths.Where(path => path.Length == 1).ToList();

        if (myEminentWinningPaths.Count != 0) return myEminentWinningPaths[0][0];

        if (oponentEminentWiningPaths.Count != 0) return oponentEminentWiningPaths[0][0];
        {
          if (myWinningPaths.Count != 0) return myWinningPaths[0][new Random().Next(myWinningPaths[0].Length)];
        }
      }
      var moves = TicTacToe.GetPossibleMoves(GameState).ToList();
      return moves[new Random().Next(moves.Count)];
    } 

    public int MakeMove() => TicTacToe.IsEmpty(GameState) ? new Random().Next(9) : SelectOptimalMove();
  }
}
