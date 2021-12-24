using System;

using UniRx;
using Zenject;

using Domain.SceneTransition;

namespace Controllers.SceneTransition {
  public class UnitySceneTransitionController : ISceneTransitionController {
    private readonly ITransitionAnimator transitionAnimator;

    private readonly Subject<Unit> transitionInCompleteSubject = new Subject<Unit>();
    private readonly Subject<Unit> transitionOutCompleteSubject = new Subject<Unit>();

    public UnitySceneTransitionController(ITransitionAnimator transAnimator)
      => this.transitionAnimator = transAnimator;

    public IObservable<Unit> OnTransitionInComplete => this.transitionInCompleteSubject;
    public IObservable<Unit> OnTransitionOutComplete => this.transitionOutCompleteSubject;

    public void PerformTransitionIn() {
      this.transitionAnimator.TransitionIn(() => {
        this.transitionInCompleteSubject.OnCompleted();
      });
    }

    public void PerformTransitionOut() {
      this.transitionAnimator.TransitionOut(() => {
        this.transitionOutCompleteSubject.OnCompleted();
      });
    }
  }
}
