using System.Collections.Generic;
using Controllers.Game.Views;
using Domain.Game.Events;
using Domain.Game.Strategy;
using Domain.Game.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Controllers.Game.Units {
  public class EnemyUnitController : UnitController, IEnemyUnitController {
    [SerializeField] private bool isKing;
    [SerializeField] private Color kingColor;

    [Inject] private IUnitView unitView;
    [Inject] private IMoveStrategy moveStrategy;
    private readonly List<IUnitController> enemiesInSightRange = new List<IUnitController>();

    public bool IsKing => this.isKing;

    public void OnUnitSightRangeEnter(IUnitController unit) {
      this.enemiesInSightRange.Add(unit);
    }

    public void OnUnitSightRangeExit(IUnitController unit) {
      this.enemiesInSightRange.Remove(unit);
    }

    protected override void onInitialize() {
      if (this.isKing) {
        this.unitView.SetTitleColor(this.kingColor);
      }

      this.OnDeath
        .Subscribe(
          _ => { },
          () => {
            this.signalBus.Fire(new EnemyUnitKilledEvent(this.IsKing));
            Destroy(this.gameObject);
          });
    }

    protected override void handleMovement() {
      var dir = this.moveStrategy.GetMovementVector(this.enemiesInSightRange);

      if (dir.sqrMagnitude < 0.01f) {
        this.rigidbody2D.velocity = Vector2.zero;
      } else {
        this.rigidbody2D.velocity = dir.normalized * this.unitData.Speed;
      }
    }
  }
}