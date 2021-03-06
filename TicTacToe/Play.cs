﻿using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
  public class Play
  {
    public List<Turn> Turns { get; set; }
    public int Outcome { get; set; }

    public Play()
    {
      Turns = new List<Turn>();
    }

    public Play(IEnumerable<string> states)
    {
      Turns = states.Select(state => new Turn { GameState = new State(state) }).ToList();
    }

    public bool Has(State state) => Turns.Any(turn => turn.GameState.Equals(state));
  }
}