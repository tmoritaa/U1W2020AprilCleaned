using System;
using System.Collections.Generic;

using UniRx;
using UnityEngine.SceneManagement;

using Domain.SceneTransition;
using Zenject;

namespace Infrastructure {
  public class UnitySceneTransitioner : ISceneTransitioner {
    private readonly ISceneTransitionController controller;
    private readonly SignalBus signalBus;

    public UnitySceneTransitioner(ISceneTransitionController controller, SignalBus signalBus) {
      this.controller = controller;
      this.signalBus = signalBus;
    }

    public void TransitionToScene(string id) {
      this.signalBus.Fire<TransitionOutEvent>();

      this.controller
        .OnTransitionOutComplete
        .Subscribe(
          _ => {},
          () => SceneManager.LoadScene(id));

      this.controller.PerformTransitionOut();
    }
  }
}