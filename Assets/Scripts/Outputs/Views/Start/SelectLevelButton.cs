using System.Collections;
using System.Collections.Generic;
using Domain;
using Domain.SceneTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views.Start {
  public class SelectLevelButton : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private GameObject stamp;
    [Inject] private readonly ISceneTransitioner sceneTransitioner;

    public void SetLevelData(LevelData levelData) {
      this.text.text = string.Format("～{0}～", levelData.LevelTitle);
      this.button.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene(levelData.LevelName));

      bool showStamp = PlayerPrefs.GetInt(levelData.GetPlayerPrefStr(), 0) != 0;
      this.stamp.gameObject.SetActive(showStamp);
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, SelectLevelButton> {
    }
  }
}
