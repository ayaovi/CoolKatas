using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
  public class State
  {
    public Field[][] Fields { get; set; } = {
        new[] {Field.Empty, Field.Empty, Field.Empty},
        new[] {Field.Empty, Field.Empty, Field.Empty},
        new[] {Field.Empty, Field.Empty, Field.Empty}
      };

    public State(){}

    public State(State state)
    {
      for (var i = 0; i < 3; i++)
      {
        for (var j = 0; j < 3; j++)
        {
          Fields[i][j] = state.Fields[i][j];
        }
      }
    }

    public State(string strSate)
    {
      var rows = strSate.Split('|');
      for (var i = 0; i < rows.Length; i++)
      {
        for (var j = 0; j < rows[i].Length; j++)
        {
          switch (rows[i][j])
          {
            case 'x':
              Fields[i][j] = Field.X;
              break;
            case 'o':
              Fields[i][j] = Field.O;
              break;
          }
        }
      }
    }

    public IEnumerable<Field> FlattenedFields() => Fields.SelectMany(x=>x);

    public static string FieldToString(Field field)
    {
      switch (field)
      {
        case Field.Empty:
          return ".";
        case Field.X:
          return "x";
        case Field.O:
          return "o";
        default:
          throw new ArgumentOutOfRangeException(nameof(field), field, null);
      }
    }

    public override bool Equals(object obj)
    {
      var state = obj as State;
      if (state == null) return false;
      return state.FlattenedFields()
                  .Zip(FlattenedFields(), Tuple.Create)
                  .All(x => x.Item1 == x.Item2);
    }

    public override string ToString()
    {
      return string.Join("|", Fields.Select(row => string.Concat(row.Select(FieldToString))));
    }
  }
}