using Domain.Game.Units;

namespace Domain.Game.Events {
  public struct PlayerUnitKilledEvent {
    public readonly PlayerUnitData.PlayerType PlayerType;

    public PlayerUnitKilledEvent(PlayerUnitData.PlayerType playerType) {
      this.PlayerType = playerType;
    }
  }
}