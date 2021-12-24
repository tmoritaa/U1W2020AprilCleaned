using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Domain.Game.Events;
using Domain.SceneTransition;
using UnityEngine;
using Zenject;

namespace Outputs.Audio {
  public class GameBgmPlayer : MonoBehaviour, IInitializable, IDisposable {
    [Inject] private SignalBus signalBus;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource combatStartAudioSource;

    public void Initialize() {
      this.signalBus.Subscribe<GameplayStartEvent>(this.playBgm);
      this.signalBus.Subscribe<StageFinishedEvent>(this.stopBgm);
      this.signalBus.Subscribe<FirstTimeDamageDealtEvent>(this.onFirstTimeDamageDealt);
      this.signalBus.Subscribe<TransitionOutEvent>(this.stopBgm);
    }

    public void Dispose() {
      this.signalBus.Unsubscribe<GameplayStartEvent>(this.playBgm);
      this.signalBus.Unsubscribe<StageFinishedEvent>(this.stopBgm);
      this.signalBus.Unsubscribe<FirstTimeDamageDealtEvent>(this.onFirstTimeDamageDealt);
      this.signalBus.Unsubscribe<TransitionOutEvent>(this.stopBgm);
    }

    private void onFirstTimeDamageDealt() {
      if (!this.combatStartAudioSource.isPlaying) {
        this.combatStartAudioSource.Play();
      }
    }

    private void playBgm() {
      this.bgmSource.Play();
    }

    private void stopBgm() {
      this.bgmSource.DOFade(0, 0.5f)
        .OnComplete(this.bgmSource.Stop);

      if (this.combatStartAudioSource.isPlaying) {
        this.combatStartAudioSource.DOFade(0, 0.3f).OnComplete(this.combatStartAudioSource.Stop);
      }
    }
  }
}
