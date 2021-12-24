using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Domain;
using Domain.Game.Controllers;
using Domain.SceneTransition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views.Game {
  public class GameCompleteView : MonoBehaviour, IGameCompleteController, IInitializable {
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button nextStageButton;
    [SerializeField] private TextMeshProUGUI nextStageButtonText;
    [SerializeField] private Button returnToHomeButton;
    [SerializeField] private GameObject stamp;
    [SerializeField] private TextMeshProUGUI stampText;
    [SerializeField] private RectTransform scrollTopPart;
    [SerializeField] private RectTransform scrollBackground;

    [Inject] private ISceneTransitioner sceneTransitioner;
    [Inject(Id = "cur_level_info")] private LevelData curLevelData;
    [Inject(Id = "next_level_info")] private LevelData nextLevelData;
    [Inject] private ISeAudioPlayer seAudioPlayer;

    public void Initialize() {
      this.returnToHomeButton.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene("Start"));

      this.title.gameObject.SetActive(false);
      this.stamp.gameObject.SetActive(false);
      this.returnToHomeButton.gameObject.SetActive(false);
      this.nextStageButton.gameObject.SetActive(false);
      this.scrollTopPart.gameObject.SetActive(false);
      this.scrollBackground.gameObject.SetActive(false);
    }

    public void Show(bool hasWon) {
      this.gameObject.SetActive(true);

      this.title.text = hasWon ? "今宵は宴じゃ！" : "この恥晒しが!";

      if (hasWon) {
        this.stampText.text = "覇";

        var nextStageExists = !string.IsNullOrEmpty(this.nextLevelData.LevelName);
        if (nextStageExists) {
          this.nextStageButtonText.text = "次";
          this.nextStageButton.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene(this.nextLevelData.LevelName));
        }
      } else {
        this.nextStageButtonText.text = "再挑戦";
        this.stampText.text = "敗";
        this.nextStageButton.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene(this.curLevelData.LevelName));
      }

      this.playShowAnimation(hasWon);
    }

    public void Hide() {
      this.gameObject.SetActive(false);
    }

    private void playShowAnimation(bool hasWon) {
      this.scrollTopPart.transform.localScale = new Vector3(100, 100, 1);
      this.title.transform.localScale = new Vector3(15, 15, 1);
      this.stamp.transform.localScale = new Vector3(20, 20, 1);
      this.scrollBackground.transform.localScale = new Vector3(1, 0, 1);

      this.scrollTopPart.transform.gameObject.SetActive(true);

      DOTween.Sequence()
        .Append(this.scrollTopPart.transform.DOScale(new Vector3(1, 1, 1), 0.2f))
        .AppendCallback(() => this.seAudioPlayer.Play("old_timey_shake"))
        .Append(this.scrollTopPart.transform.DOShakePosition(0.3f, new Vector3(75, 5, 0), 20))
        .AppendCallback(() => this.scrollBackground.transform.gameObject.SetActive(true))
        .Append(this.scrollBackground.transform.DOScale(new Vector3(1, 1, 1), 0.6f)).SetEase(Ease.InQuad)
        .AppendCallback(() => this.title.gameObject.SetActive(true))
        .Append(this.title.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InQuad))
        .AppendCallback(() => this.seAudioPlayer.Play("drum_single"))
        .Append(this.title.transform.DOShakePosition(0.3f, new Vector3(75, 5, 0), 20))
        .AppendInterval(0.5f)
        .AppendCallback(() => this.stamp.gameObject.SetActive(true))
        .Append(this.stamp.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InQuad))
        .AppendCallback(() => this.seAudioPlayer.Play("drum_double"))
        .Append(this.stamp.transform.DOShakePosition(0.3f, new Vector3(75, 5, 0), 20))
        .AppendInterval(0.25f)
        .AppendCallback(() => {
          if (hasWon) {
            var nextStageExists = !string.IsNullOrEmpty(this.nextLevelData.LevelName);
            this.nextStageButton.gameObject.SetActive(nextStageExists);
          } else {
            this.nextStageButton.gameObject.SetActive(true);
          }

          this.returnToHomeButton.gameObject.SetActive(true);
        });
    }

  }
}
