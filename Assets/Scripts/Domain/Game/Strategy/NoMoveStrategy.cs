using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;

namespace Domain.Game.Strategy {
  public class NoMoveStrategy : MonoBehaviour, IMoveStrategy {
    public Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange) {
      return Vector2.zero;
    }
  }
}