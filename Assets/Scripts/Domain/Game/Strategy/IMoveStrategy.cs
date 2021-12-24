using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;

namespace Domain.Game.Strategy {
  public interface IMoveStrategy {
    Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange);
  }
}