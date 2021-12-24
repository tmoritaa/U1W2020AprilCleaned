using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

using Controllers;
using Controllers.Game.Input;
using Controllers.Game.Intro;
using Controllers.Game.Units;
using Domain;
using Domain.Game;
using Domain.Game.Events;
using Domain.Game.States;
using Domain.Game.Units;
using UnityEngine.SceneManagement;

namespace Installers.Game {
  public class GameMonoInstaller : MonoInstaller {
    public override void InstallBindings()  {
      this.Container.DeclareSignal<UnitsClickedEvent>();
      this.Container.DeclareSignal<GameplayStartEvent>();
      this.Container.DeclareSignal<PlayerUnitKilledEvent>();
      this.Container.DeclareSignal<EnemyUnitKilledEvent>();
      this.Container.DeclareSignal<StageFinishedEvent>();
      this.Container.DeclareSignal<FirstTimeDamageDealtEvent>();

      this.Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

      this.Container.BindInterfacesAndSelfTo<GameSetupState>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<GameIntroState>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<GamePlayState>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<GameCompleteWonState>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<GameCompleteLostState>().AsSingle();

      this.Container.BindInterfacesTo<GameInputService>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<GameContext>().AsSingle().NonLazy();

      this.Container.BindInterfacesAndSelfTo<IntroController>().AsSingle();

      this.Container.BindInterfacesAndSelfTo<GameStarter<GameStateMachine>>().AsSingle().NonLazy();

      var playerUnits = FindObjectsOfType<PlayerUnitController>().Cast<IPlayerUnitController>();
      this.Container.BindInstance<IEnumerable<IPlayerUnitController>>(playerUnits).WithId("initial_player_list");

      var numEnemyUnits = FindObjectsOfType<EnemyUnitController>().Length;
      this.Container.BindInstance<int>(numEnemyUnits).WithId("num_enemy_units");
    }
  }
}
