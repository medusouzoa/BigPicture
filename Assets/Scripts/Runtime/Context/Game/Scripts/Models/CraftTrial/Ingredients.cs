using System;
using System.Collections.Generic;

namespace Runtime.Context.Game.Scripts.Models.CraftTrial
{
  [Serializable]
  public class Ingredients
  {
    private Dictionary<int, int> list;

    public Ingredients()
    {
      this.list = new Dictionary<int, int>();
    }

    public Ingredients(Dictionary<int, int> ingredientsList)
    {
      this.list = ingredientsList;
    }

    public Ingredients Add(int lootId, int quantity = 1)
    {
      this.list.Add(lootId, quantity);

      return this;
    }

    public Ingredients Remove(int lootId, int quantity = 1)
    {
      if (this.list.ContainsKey(lootId))
      {
        if (this.list[lootId] - quantity >= 1)
        {
          this.list[lootId] -= quantity;
        }
        else
        {
          this.list.Remove(lootId);
        }
      }

      return this;
    }

    public Dictionary<int, int> Contents
    {
      get { return this.list; }
    }
  }
}