namespace Runtime.Context.Game.Scripts.Vo
{
  public class PlayerData
  {
    public float money;

    public void AddMoney(float amount)
    {
      money += amount;
    }

    public void SubtractMoney(float amount)
    {
      money -= amount;
    }
  }
}