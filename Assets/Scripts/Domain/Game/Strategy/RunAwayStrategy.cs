using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Domain.Game.Strategy {
  public class RunAwayStrategy : MonoBehaviour, IMoveStrategy {
    [Inject] private IUnitController self;

    public Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange) {
      var oppDir = Vector2.zero;

      foreach (var enemy in enemiesInSightRange) {
        var diff = (this.self.WorldPos - enemy.WorldPos).normalized;
        oppDir += diff;
      }

      return oppDir.normalized;
    }
  }
}