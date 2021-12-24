using Domain.Game.Controllers;
using Domain.SceneTransition;
using Domain.States;
using UniRx;
using UnityEngine;
using Zenject;

namespace Domain.Game.States {
  public class GameIntroState : BaseGameState {
    private readonly ISceneTransitionController transitionController;
    private readonly IIntroController introController;
    private readonly LevelData curLevelData;

    public bool Complete { get; private set; }

    public GameIntroState(ISceneTransitionController transitionController, IIntroController introController, [Inject(Id = "cur_level_info")]LevelData curLevelData) {
      this.transitionController = transitionController;
      this.introController = introController;
      this.curLevelData = curLevelData;
    }

    protected override void onEnter() {
      this.transitionController.OnTransitionInComplete
        .Subscribe(
          _ => { },
          () => this.introController.PlayIntro(this.curLevelData.LevelTitle, this.curLevelData.LevelDescription, () => this.Complete = true));

      this.transitionController.PerformTransitionIn();
    }

    protected override void onExit() {
    }
  }
}