using Domain.Game.Controllers;
using Domain.States;
using UnityEngine;

namespace Domain.Game.States {
  public class GameSetupState : BaseGameState {
    private readonly IGameCompleteController completeController;

    public GameSetupState(IGameCompleteController controller)
      => this.completeController = controller;

    protected override void onEnter() {
      this.completeController.Hide();
    }

    protected override void onExit() {
    }
  }
}