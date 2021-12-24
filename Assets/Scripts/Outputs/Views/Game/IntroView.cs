using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.Game.Intro;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views.Game {
  public class IntroView : MonoBehaviour, IIntroView, IInitializable {
    [Inject(Id = "overlay")] private readonly Image overlayImage;
    [Inject(Id = "title")] private readonly TextMeshProUGUI titleText;
    [Inject(Id = "description")] private readonly TextMeshProUGUI descriptionText;
    [Inject] private readonly ISeAudioPlayer seAudioPlayer;

    public void Initialize() {
      this.titleText.gameObject.SetActive(false);
      this.descriptionText.gameObject.SetActive(false);
    }

    public void PlayIntro(string title, string description, Action onComplete) {
      this.titleText.text = title;
      this.descriptionText.text = description;

      this.titleText.transform.localScale = new Vector3(10, 10, 1);
      this.descriptionText.transform.localScale = new Vector3(10, 10, 1);

      this.titleText.gameObject.SetActive(true);

      var sequence =
        DOTween.Sequence()
          .Append(this.titleText.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InQuad))
          .AppendCallback(() => this.seAudioPlayer.Play("drum_single"))
          .Append(this.titleText.transform.DOShakePosition(0.3f, new Vector3(75, 5, 0), 20))
          .AppendInterval(0.5f)
          .AppendCallback(() => this.descriptionText.gameObject.SetActive(true))
          .Append(this.descriptionText.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InQuad))
          .AppendCallback(() => this.seAudioPlayer.Play("drum_double"))
          .Append(this.descriptionText.transform.DOShakePosition(0.3f, new Vector3(75, 5, 0), 20))
          .AppendInterval(0.5f)
          .Append(this.overlayImage.DOFade(0, 0.4f))
          .AppendCallback(() => this.overlayImage.gameObject.SetActive(false))
          .AppendCallback(() => onComplete());
    }
  }
}
