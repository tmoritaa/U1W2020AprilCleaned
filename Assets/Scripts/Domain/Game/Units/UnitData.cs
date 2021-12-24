namespace Domain.Game.Units {
  public abstract class UnitData : IUnitData {
    protected UnitBaseData unitBaseData;

    public UnitData(UnitBaseData unitBaseData)
      => this.unitBaseData = unitBaseData;

    public abstract float Speed { get; }
    public abstract int Damage { get; }
    public abstract int Health { get; }
    public abstract float AttackCooldown { get; }
    public abstract int Defense { get; }
    public bool IsTrueDamage => this.unitBaseData.IsTrueDamage;
  }
}