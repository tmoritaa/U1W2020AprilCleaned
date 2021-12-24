namespace Domain.Game.Units {
  public interface IUnitData {
    float Speed { get; }
    int Damage { get; }
    int Health { get; }
    float AttackCooldown { get; }
    int Defense { get; }
    bool IsTrueDamage { get; }
  }
}