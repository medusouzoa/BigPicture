using Runtime.Context.Game.Scripts.Vo;

namespace Runtime.Context.Game.Scripts.Models.DamageModel
{
  public interface IDamageModel
  {
    int damage { get; set; }
    ZombieStats zombie { get; set; }
  }
}