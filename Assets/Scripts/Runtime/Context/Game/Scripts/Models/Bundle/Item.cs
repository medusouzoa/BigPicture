namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class Item
  {
    public enum ItemType
    {
      None,
      Wrench,
      Stone,
      Stick
    }

    public ItemType itemType;
    public int amount;
    
  }
}