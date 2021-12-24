using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Game.Events;
using Domain.Game.Input;
using Domain.Game.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Domain.Game {
  public class GameContext : IInitializable, IDisposable {
    private readonly IGameInputService inputService;
    private readonly SignalBus signalBus;
    private readonly LevelData.GameTypes curGameType;

    private readonly CompositeDisposable disposables = new CompositeDisposable();

    private List<IPlayerUnitController> selectedUnits = new List<IPlayerUnitController>();
    private CompositeDisposable unitDisposables = new CompositeDisposable();

    private readonly IEnumerable<IPlayerUnitController> initialPlayerList;
    private int numPlayerUnitsLeft;
    private int numPlayerKingKillerUnitsLeft;
    private int numEnemyUnitsLeft;

    private bool gameOver = false;

    private bool isKingKillerMap => this.curGameType == LevelData.GameTypes.KingKiller;

    public GameContext(IGameInputService inputService, SignalBus signalBus, LevelData.GameTypes gameType, [Inject(Id = "initial_player_list")]IEnumerable<IPlayerUnitController> initialPlayerList, [Inject(Id = "num_enemy_units")]int numEnemyUnitsLeft) {
      this.inputService = inputService;
      this.signalBus = signalBus;
      this.curGameType = gameType;
      this.initialPlayerList = initialPlayerList;
      this.numPlayerUnitsLeft = initialPlayerList.Count();
      this.numEnemyUnitsLeft = numEnemyUnitsLeft;
    }

    public void Initialize() {
      this.signalBus.Subscribe<UnitsClickedEvent>(this.onUnitClicked);
      this.signalBus.Subscribe<EnemyUnitKilledEvent>(this.onEnemyUnitKilled);
      this.signalBus.Subscribe<PlayerUnitKilledEvent>(this.onPlayerUnitKilled);

      this.inputService.OnRightClick
        .Where(_ => !this.gameOver)
        .Subscribe(this.onMoveInput)
        .AddTo(this.disposables);

      if (this.isKingKillerMap) {
        this.numPlayerKingKillerUnitsLeft = this.initialPlayerList.Count(pu => pu.PlayerType == PlayerUnitData.PlayerType.KingKiller);
      }
    }

    public void Dispose() {
      this.signalBus.Unsubscribe<UnitsClickedEvent>(this.onUnitClicked);
      this.signalBus.Unsubscribe<EnemyUnitKilledEvent>(this.onEnemyUnitKilled);
      this.signalBus.Unsubscribe<PlayerUnitKilledEvent>(this.onPlayerUnitKilled);

      this.disposables.Clear();
    }

    private void onUnitClicked(UnitsClickedEvent evt) {
      if (this.gameOver) {
        return;
      }

      this.unitDisposables.Clear();

      this.selectedUnits.ForEach(su => su.OnUnitUnselected());

      this.selectedUnits = evt.ClickedUnits.ToList();
      foreach (var unit in this.selectedUnits) {
        unit.OnDeath
          .Subscribe(
            _ => { },
            () => this.selectedUnits.Remove(unit))
          .AddTo(this.unitDisposables);

        unit.OnUnitSelected();
      }
    }

    private void onMoveInput(Vector2 targetPos) {
      if (this.selectedUnits.Count == 0) {
        return;
      }

      var centroid = Vector2.zero;
      this.selectedUnits.ForEach(u => centroid += u.WorldPos);
      centroid /= this.selectedUnits.Count;

      foreach (var unit in this.selectedUnits) {
        var unitTargetPos = (unit.WorldPos - centroid) + targetPos;
        unit.MoveTo(unitTargetPos);
      }
    }

    private void onEnemyUnitKilled(EnemyUnitKilledEvent evt) {
      if (this.gameOver) {
        return;
      }

      this.numEnemyUnitsLeft -= 1;

      if ((this.isKingKillerMap && evt.IsKing) || this.numEnemyUnitsLeft <= 0) {
        this.signalStageWon();
      }
    }

    private void onPlayerUnitKilled(PlayerUnitKilledEvent evt) {
      if (this.gameOver) {
        return;
      }

      this.numPlayerUnitsLeft -= 1;

      if (this.isKingKillerMap) {
        if (evt.PlayerType == PlayerUnitData.PlayerType.KingKiller) {
          this.numPlayerKingKillerUnitsLeft -= 1;
        }

        if (this.numPlayerKingKillerUnitsLeft <= 0) {
          this.signalStageLost();
        }
      }

      if (this.numPlayerUnitsLeft <= 0) {
        this.signalStageLost();
      }
    }

    private void signalStageWon() {
      this.gameOver = true;
      this.signalBus.Fire(new StageFinishedEvent(true));
    }

    private void signalStageLost() {
      this.gameOver = true;
      this.signalBus.Fire(new StageFinishedEvent(false));
    }
  }
}