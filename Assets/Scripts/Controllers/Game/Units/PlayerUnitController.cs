using System;
using System.Collections.Generic;
using Controllers.Game.Views;
using Domain.Game.Events;
using Domain.Game.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Controllers.Game.Units {
  public class PlayerUnitController : UnitController, IPlayerUnitController {
    [Inject] private PlayerUnitData playerUnitData;
    [Inject] private IEffectRingView effectRingView;
    [Inject] private IPlayerUnitView playerUnitView;

    private Vector2? moveTargetPos;

    public PlayerUnitData.PlayerType PlayerType => this.playerUnitData.UnitType;

    public void MoveTo(Vector2 targetPos) {
      this.moveTargetPos = targetPos;
    }

    public void OnUnitSelected() {
      this.playerUnitView.SetUnitSelectOutline(true);
    }

    public void OnUnitUnselected() {
      this.playerUnitView.SetUnitSelectOutline(false);
    }

    public void OnEnterUnitEffectRange(PlayerUnitController playerUnit) {
      if (playerUnit != this) {
        playerUnit.AddPlayerEffect(this.playerUnitData.UnitType);
      }
    }

    public void OnExitUnitEffectRange(PlayerUnitController playerUnit) {
      if (playerUnit != this) {
        playerUnit.RemovePlayerEffect(this.playerUnitData.UnitType);
      }
    }

    public void AddPlayerEffect(PlayerUnitData.PlayerType playerType) {
      this.playerUnitData.AddPlayerEffect(playerType);
      this.effectRingView.UpdateEffectRing(this.playerUnitData.PlayerEffects);
    }

    public void RemovePlayerEffect(PlayerUnitData.PlayerType playerType) {
      this.playerUnitData.RemovePlayerEffect(playerType);
      this.effectRingView.UpdateEffectRing(this.playerUnitData.PlayerEffects);
    }

    protected override void onInitialize() {
      this.unitView.SetTitle(PlayerUnitData.GetTitleFromPlayerType(this.playerUnitData.UnitType));
      this.playerUnitView.SetUnitSelectOutline(false);
      this.effectRingView.UpdateEffectRing(new List<PlayerUnitData.PlayerType>());

      this.OnDeath
        .Subscribe(
          _ => { },
          () => {
            this.signalBus.Fire(new PlayerUnitKilledEvent(this.PlayerType));
            Destroy(this.gameObject);
          });
    }

    protected override void handleMovement() {
      if (this.moveTargetPos.HasValue) {
        var diff = this.moveTargetPos.Value - (Vector2)this.transform.position;

        if (diff.sqrMagnitude < 0.01f) {
          this.moveTargetPos = null;
          this.rigidbody2D.velocity = Vector2.zero;
        } else {
          this.rigidbody2D.velocity = diff.normalized * this.unitData.Speed;
        }
      }
    }
  }
}
