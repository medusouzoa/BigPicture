using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;

namespace Runtime.Context.Game.Scripts.Models.GameModel
{
  public interface IGameModel
  {
    List<List<GridVo>> grids { get; }
    
    void Setup();

    void StartGame();
    void OnPostConstruct();
  }
}