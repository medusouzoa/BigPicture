using Runtime.Context.Game.Scripts.Vo;

namespace Runtime.Context.Game.Scripts.Models.DamageModel
{
  public class DamageModel: IDamageModel
  {
    public int damage { get; set; }
    public   ZombieStats zombie { get; set; }
  }
}