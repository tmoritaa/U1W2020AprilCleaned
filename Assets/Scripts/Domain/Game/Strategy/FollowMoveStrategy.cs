using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Domain.Game.Strategy {
  public class FollowMoveStrategy : MonoBehaviour, IMoveStrategy {
    [Inject] private IUnitController self;

    public Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange) {
      var dir = Vector2.zero;

      if (enemiesInSightRange.Count > 0) {
        dir = StrategyUtilities.FindClosestUnit(this.self.WorldPos, enemiesInSightRange).WorldPos - this.self.WorldPos;
      }

      return dir;
    }
  }
}