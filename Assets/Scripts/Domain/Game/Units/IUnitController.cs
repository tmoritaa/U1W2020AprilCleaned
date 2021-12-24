using System;
using UniRx;
using UnityEngine;

namespace Domain.Game.Units {
  public interface IUnitController {
    IObservable<Unit> OnDeath { get; }

    Vector2 WorldPos { get; }

    void DamageDealt(int dmg, bool isTrueDamage);

    void OnUnitEnterAttackRange(IUnitController unit);
    void OnUnitExitAttackRange(IUnitController unit);
  }
}