using System;
using System.Collections.Generic;

using UniRx;

namespace Domain.SceneTransition {
  public interface ISceneTransitionController {
    IObservable<Unit> OnTransitionInComplete { get; }
    IObservable<Unit> OnTransitionOutComplete { get; }

    void PerformTransitionIn();
    void PerformTransitionOut();
  }
}