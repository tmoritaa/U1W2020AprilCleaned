using System.Collections.Generic;
using Domain.Game.Units;

namespace Controllers.Game.Views {
  public interface IEffectRingView {
    void UpdateEffectRing(List<PlayerUnitData.PlayerType> playerTypes);
  }
}