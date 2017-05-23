using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
  public class Play
  {
    public List<Turn> Turns { get; set; } = new List<Turn>();
    public int Outcome { get; set; }

    public bool Has(State state) => Turns.Any(turn => turn.GameState.Equals(state));
  }
}