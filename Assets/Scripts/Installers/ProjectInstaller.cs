using Controllers.SceneTransition;
using Domain.SceneTransition;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Installers {
  public class ProjectInstaller : MonoInstaller {
    public override void InstallBindings() {
      SignalBusInstaller.Install(this.Container);
      this.Container.DeclareSignal<TransitionOutEvent>();

      this.Container.BindInterfacesTo<UniTaskStateMachineTransitionPoller>().AsTransient();

      this.Container.BindInterfacesTo<UnitySceneTransitionController>().AsSingle().MoveIntoAllSubContainers();
      this.Container.BindInterfacesTo<UnitySceneTransitioner>().AsSingle().MoveIntoAllSubContainers();
    }
  }
}
