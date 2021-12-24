using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Domain.Game.Strategy {
  public class HoldPositionStrategy : MonoBehaviour, IMoveStrategy, IInitializable {
    [Inject] private IUnitController self;

    private Vector2 holdPos;

    public void Initialize() {
      this.holdPos = this.self.WorldPos;
    }

    public Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange) {
      var dir = Vector2.zero;

      var distToPivot = this.holdPos - this.self.WorldPos;

      if (distToPivot.sqrMagnitude > 0.05f) {
        dir = distToPivot.normalized;
      }

      return dir;
    }

  }
}