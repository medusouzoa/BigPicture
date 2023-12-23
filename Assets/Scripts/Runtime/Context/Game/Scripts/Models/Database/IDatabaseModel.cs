using System.Collections;

namespace Runtime.Context.Game.Scripts.Models.Database
{
  public interface IDatabaseModel
  {
    IEnumerator SaveItem(string itemName, int amount);
  }
}