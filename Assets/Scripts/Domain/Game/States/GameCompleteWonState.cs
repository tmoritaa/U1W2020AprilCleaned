using Domain.Game.Controllers;
using Domain.States;
using UnityEngine;
using Zenject;

namespace Domain.Game.States {
  public class GameCompleteWonState : BaseGameState {
    private readonly IGameCompleteController gameCompleteController;
    private readonly LevelData curLevelData;

    public GameCompleteWonState(IGameCompleteController controller, [Inject(Id = "cur_level_info")]LevelData curLevelData)
      => (this.gameCompleteController, this.curLevelData) = (controller, curLevelData);

    protected override void onEnter() {
      this.gameCompleteController.Show(true);

      PlayerPrefs.SetInt(this.curLevelData.GetPlayerPrefStr(), 1);
    }

    protected override void onExit() {
    }
  }
}