using Controllers.Game.Units;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Controllers.Game {
  public class UnitEffectRangePropagator : MonoBehaviour {
    [Inject] private PlayerUnitController unitController;

    private void OnTriggerEnter2D(Collider2D other) {
      this.unitController.OnEnterUnitEffectRange(other.GetComponent<PlayerUnitController>());
    }

    private void OnTriggerExit2D(Collider2D other) {
      this.unitController.OnExitUnitEffectRange(other.GetComponent<PlayerUnitController>());
    }
  }
}