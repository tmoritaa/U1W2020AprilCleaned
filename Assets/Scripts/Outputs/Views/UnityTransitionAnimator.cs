using System;
using Controllers.SceneTransition;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views {
  public class UnityTransitionAnimator : MonoBehaviour, ITransitionAnimator {
    [SerializeField] private float transitionDuration;

    [SerializeField] private Image foreground;

    public void TransitionIn(Action onComplete) {
      this.foreground.gameObject.SetActive(true);
      var color = this.foreground.color;
      color.a = 1;
      this.foreground.color = color;

      this.foreground
        .DOFade(0, this.transitionDuration)
        .OnComplete(() => {
          this.foreground.gameObject.SetActive(false);
          onComplete();
        });
    }

    public void TransitionOut(Action onComplete) {
      this.foreground.gameObject.SetActive(true);
      var color = this.foreground.color;
      color.a = 0;
      this.foreground.color = color;
      this.foreground
        .DOFade(1, this.transitionDuration)
        .OnComplete(() => {
          onComplete();
        });
    }
  }
}