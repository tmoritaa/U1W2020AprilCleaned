using UnityEngine;

namespace Domain.Game.Units {
  public interface IPlayerUnitController : IUnitController {
    void MoveTo(Vector2 targetPos);
    void OnUnitSelected();
    void OnUnitUnselected();

    PlayerUnitData.PlayerType PlayerType { get; }
  }
}