using System;

namespace Controllers.SceneTransition {
  public interface ITransitionAnimator {
    void TransitionIn(Action onComplete);
    void TransitionOut(Action onComplete);
  }
}