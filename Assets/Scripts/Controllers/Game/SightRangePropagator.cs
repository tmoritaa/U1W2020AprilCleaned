using Controllers.Game.Units;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Controllers.Game {
  public class SightRangePropagator : MonoBehaviour {
    [Inject] private IEnemyUnitController unitController;

    private void OnTriggerEnter2D(Collider2D other) {
      this.unitController.OnUnitSightRangeEnter(other.GetComponent<UnitController>());
    }

    private void OnTriggerExit2D(Collider2D other) {
      this.unitController.OnUnitSightRangeExit(other.GetComponent<UnitController>());
    }
  }
}
