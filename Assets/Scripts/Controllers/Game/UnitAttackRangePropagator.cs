using Controllers.Game.Units;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Controllers.Game {
  public class UnitAttackRangePropagator : MonoBehaviour {
    [Inject] private IUnitController unitController;

    private void OnTriggerEnter2D(Collider2D other) {
      this.unitController.OnUnitEnterAttackRange(other.GetComponent<UnitController>());
    }

    private void OnTriggerExit2D(Collider2D other) {
      this.unitController.OnUnitExitAttackRange(other.GetComponent<UnitController>());
    }
  }
}
