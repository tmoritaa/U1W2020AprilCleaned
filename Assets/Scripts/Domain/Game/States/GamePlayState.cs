using System;
using Domain.Game.Events;
using Domain.Game.Input;
using Domain.States;
using UnityEngine;
using Zenject;

namespace Domain.Game.States {
  public class GamePlayState : BaseGameState {
    public bool Complete { get; private set; }
    public bool HasWon { get; private set; }

    private readonly IGameInputService inputService;
    private readonly SignalBus signalBus;

    public GamePlayState(IGameInputService inputService, SignalBus signalBus) {
      this.inputService = inputService;
      this.signalBus = signalBus;
    }

    public override void Dispose() {
      this.inputService.StopListening();
      this.signalBus.TryUnsubscribe<StageFinishedEvent>(this.onStageFinished);
    }

    protected override void onEnter() {
      this.signalBus.Subscribe<StageFinishedEvent>(this.onStageFinished);

      this.signalBus.Fire<GameplayStartEvent>();

      this.inputService.StartListening();
    }

    protected override void onExit() {
      this.Dispose();
    }

    private void onStageFinished(StageFinishedEvent evt) {
      this.Complete = true;
      this.HasWon = evt.StageWon;
    }
  }
}