namespace Runtime.Context.Game.Scripts.Models.PlayerModel
{
  public interface IPlayerModel
  {
    int money { get; set; }
    void AddMoney(int amount);
    void SubtractMoney(int amount);
  }
}