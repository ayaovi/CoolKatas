﻿using System.Collections.Generic;

namespace TicTacToe
{
  public class Play
  {
    public List<Turn> Turns { get; set; } = new List<Turn>();
    public Field Winner { get; set; }
  }
}