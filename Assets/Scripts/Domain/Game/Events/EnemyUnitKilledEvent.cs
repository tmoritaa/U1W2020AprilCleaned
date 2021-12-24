using Domain.Game.Units;

namespace Domain.Game.Events {
  public struct EnemyUnitKilledEvent {
    public readonly bool IsKing;

    public EnemyUnitKilledEvent(bool isKing) {
      this.IsKing = isKing;
    }
  }
}