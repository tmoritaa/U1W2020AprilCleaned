using System.Collections;
using System.Collections.Generic;
using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Domain.Game.Strategy {
  public class FollowUptoDistanceThenReturnMoveStrategy : MonoBehaviour, IMoveStrategy, IInitializable {
    [SerializeField] private float maxDistance;
    [SerializeField] private float stopChasingCooldown = 2;
    [Inject] private IUnitController self;

    private Vector2 origPos;

    private bool returning = false;
    private Coroutine returnCooldownCoroutine = null;

    public void Initialize() {
      this.origPos = this.self.WorldPos;
    }

    public Vector2 GetMovementVector(IList<IUnitController> enemiesInSightRange) {
      var dir = Vector2.zero;

      var distToPivot = this.origPos - this.self.WorldPos;

      if (this.returning && distToPivot.sqrMagnitude < 0.01f) {
        this.StopCoroutine(this.returnCooldownCoroutine);
        this.returnCooldownCoroutine = null;
        this.returning = false;
      } else if (this.returning || enemiesInSightRange.Count == 0 || distToPivot.sqrMagnitude > this.maxDistance * this.maxDistance) {
        dir = distToPivot;

        if (!this.returning) {
          this.returnCooldownCoroutine = this.StartCoroutine(this.cooldownToChaseAgain(this.stopChasingCooldown));
          this.returning = true;
        }
      } else if (enemiesInSightRange.Count > 0) {
        dir = StrategyUtilities.FindClosestUnit(this.self.WorldPos, enemiesInSightRange).WorldPos - this.self.WorldPos;
      }

      return dir;
    }

    private IEnumerator cooldownToChaseAgain(float length) {
      yield return new WaitForSeconds(length);
      this.returning = false;
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.yellow;
      Vector3 pos = this.self != null ? (Vector3)this.origPos : this.transform.position;
      Gizmos.DrawWireSphere(pos, this.maxDistance);
    }
  }
}