using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;

namespace Domain.Game.Strategy {
  public static class StrategyUtilities {
    public static IUnitController FindClosestUnit(Vector2 centerPos, IList<IUnitController> enemiesInSightRange) {
      if (enemiesInSightRange.Count == 0) {
        Debug.LogError("FindClosestUnit called with no enemies in sight");
        return null;
      }

      float minSqrDist = 9999999999f;
      IUnitController closestUnit = null;
      foreach (var enemy in enemiesInSightRange) {
        var dist = enemy.WorldPos - centerPos;

        if (minSqrDist > dist.sqrMagnitude) {
          minSqrDist = dist.sqrMagnitude;
          closestUnit = enemy;
        }
      }

      return closestUnit;
    }
  }
}