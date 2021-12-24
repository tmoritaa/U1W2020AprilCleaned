using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Domain.Game.Events;
using Domain.SceneTransition;
using UnityEngine;
using Zenject;

namespace Outputs.Audio {
  public class StartBgmPlayer : MonoBehaviour, IInitializable, IDisposable {
    [Inject] private SignalBus signalBus;
    [SerializeField] private AudioSource bgmSource;

    public void Initialize() {
      this.signalBus.Subscribe<TransitionOutEvent>(this.stopBgm);

      this.playBgm();
    }

    public void Dispose() {
      this.signalBus.Unsubscribe<TransitionOutEvent>(this.stopBgm);
    }

    private void playBgm() {
      this.bgmSource.Play();
    }

    private void stopBgm() {
      this.bgmSource.DOFade(0, 0.5f)
        .OnComplete(this.bgmSource.Stop);
    }
  }
}
