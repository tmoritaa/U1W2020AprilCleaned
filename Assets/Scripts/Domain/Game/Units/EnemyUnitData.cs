namespace Domain.Game.Units {
  public class EnemyUnitData : UnitData {
    public EnemyUnitData(UnitBaseData unitBaseData) : base(unitBaseData) {
    }

    public override float Speed => this.unitBaseData.Speed;
    public override int Damage => this.unitBaseData.Damage;
    public override int Health => this.unitBaseData.Health;
    public override float AttackCooldown => this.unitBaseData.AttackCooldown;
    public override int Defense => this.unitBaseData.Defense;
  }
}