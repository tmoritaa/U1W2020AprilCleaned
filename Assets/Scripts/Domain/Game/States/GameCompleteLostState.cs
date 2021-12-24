using Domain.Game.Controllers;
using Domain.States;
using UnityEngine;

namespace Domain.Game.States {
  public class GameCompleteLostState : BaseGameState {
    private readonly IGameCompleteController
      gameCompleteController;

    public GameCompleteLostState(IGameCompleteController controller)
      => this.gameCompleteController = controller;

    protected override void onEnter() {
      this.gameCompleteController.Show(false);
    }

    protected override void onExit() {
    }
  }
}