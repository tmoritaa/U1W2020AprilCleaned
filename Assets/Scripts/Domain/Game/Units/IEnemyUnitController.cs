namespace Domain.Game.Units {
  public interface IEnemyUnitController : IUnitController {
    void OnUnitSightRangeEnter(IUnitController unit);
    void OnUnitSightRangeExit(IUnitController unit);

    bool IsKing { get; }
  }
}