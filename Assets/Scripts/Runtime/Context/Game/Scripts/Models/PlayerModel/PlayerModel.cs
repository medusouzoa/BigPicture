namespace Runtime.Context.Game.Scripts.Models.PlayerModel
{
  public class PlayerModel : IPlayerModel
  {
    public int money { get; set; }

    public void AddMoney(int amount)
    {
      money += amount;
    }

    public void SubtractMoney(int amount)
    {
      money -= amount;
    }
  }
}