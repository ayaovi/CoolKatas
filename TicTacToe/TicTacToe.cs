using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
  public static class TicTacToe
  {
    public static State Play(State currentState, int move, Field tile)
    {
      if (move < 0 || move > 8)
      {
        throw new ArgumentException("Invalid Move");
      }

      if (currentState.Fields[move / 3][move % 3] != Field.Empty)
      {
        throw new ArgumentException("Move Already Made");
      }

      var nextState = new State(currentState);
      nextState.Fields[move / 3][move % 3] = tile;
      
      return nextState;
    }

    public static bool IsWin(State state)
    {
      var backwardDiagonal = state.Fields[0][0] != Field.Empty && state.Fields[0][0] == state.Fields[1][1] && state.Fields[1][1] == state.Fields[2][2];
      var forwardDiagonal = state.Fields[0][2] != Field.Empty && state.Fields[0][2] == state.Fields[1][1] && state.Fields[1][1] == state.Fields[2][0];
      var firstHorizontal = state.Fields[0][0] != Field.Empty && state.Fields[0][0] == state.Fields[0][1] && state.Fields[0][1] == state.Fields[0][2];
      var secondHorizontal = state.Fields[1][0] != Field.Empty && state.Fields[1][0] == state.Fields[1][1] && state.Fields[1][1] == state.Fields[1][2];
      var thirdHorizontal = state.Fields[2][0] != Field.Empty && state.Fields[2][0] == state.Fields[2][1] && state.Fields[2][1] == state.Fields[2][2];
      var firstVertical = state.Fields[0][0] != Field.Empty && state.Fields[0][0] == state.Fields[1][0] && state.Fields[1][0] == state.Fields[2][0];
      var secondVertical = state.Fields[0][1] != Field.Empty && state.Fields[0][1] == state.Fields[1][1] && state.Fields[1][1] == state.Fields[2][1];
      var thirdVertical = state.Fields[0][2] != Field.Empty && state.Fields[0][2] == state.Fields[1][2] && state.Fields[1][2] == state.Fields[2][2];

      return backwardDiagonal || forwardDiagonal ||
        firstHorizontal || secondHorizontal || thirdHorizontal ||
        firstVertical || secondVertical || thirdVertical;
    }

    public static bool IsEmpty(State state) => state.Fields.All(row => row.All(cell => cell == Field.Empty));

    public static bool IsFull(State state) => !state.Fields.Any(row => row.Any(cell => cell == Field.Empty));

    public static List<int[]> GetWinningPaths(State state, Field tile)
    {
      var winningPaths = new List<int[]>();
      var allWinningPaths = new[]
      {
        //Backward Diagonal [(0,0) (1,1) (2,2)]
        new[] { 0, 4, 8},
        //Forward Diagonal [(0,2) (1,1) (2,0)]
        new[] { 2, 4, 6 },
        // First Horizontal [(0,0) (0,1) (0,2)]
        new[] { 0, 1, 2 },
        // Second Horizontal [(1,0) (1,1) (1,2)]
        new[] { 3, 4, 5 },
        // Third Horizontal [(2,0) (2,1) (2,2)]
        new[] { 6, 7, 8 },
        // First Vertical [(0,0) (1,0) (2,0)]
        new[] { 0, 3, 6 },
        // Second Vertical [(0,1) (1,1) (2,1)]
        new[] { 1, 4, 7 },
        // Third Vertical [(0,2) (1,2) (2,2)]
        new[] { 2, 5, 8 }
      };
      
      for (var i = 0; i < state.Fields.Length; i++)
      {
        for (var j = 0; j < state.Fields[i].Length; j++)
        {
          if (state.Fields[i][j] == Field.Empty) continue;

          winningPaths.AddRange(from path in allWinningPaths
            where path.Contains(i * 3 + j)
            let isWinningPath =
            (state.Fields[path[0] / 3][path[0] % 3] == Field.Empty || state.Fields[path[0] / 3][path[0] % 3] == tile) &&
            (state.Fields[path[1] / 3][path[1] % 3] == Field.Empty || state.Fields[path[1] / 3][path[1] % 3] == tile) &&
            (state.Fields[path[2] / 3][path[2] % 3] == Field.Empty || state.Fields[path[2] / 3][path[2] % 3] == tile)
            where isWinningPath
            select path.Where(x => state.Fields[x / 3][x % 3] == Field.Empty && x != i * 3 + j).ToArray());
        }
      }
      
      return winningPaths;
    }
    
    public static IEnumerable<int> GetPossibleMoves(State state) => 
      state.Fields.SelectMany(row => row)
           .Select((cell, index) => new {cell, index})
           .Where(tile => tile.cell == Field.Empty)
           .Select(tile => tile.index);
  }
}