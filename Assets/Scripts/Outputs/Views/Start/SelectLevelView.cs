using System.Collections;
using System.Collections.Generic;
using Domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views.Start {
  public class SelectLevelView : MonoBehaviour, IInitializable {
    [SerializeField] private Transform buttonRoot;
    [SerializeField] private ScrollRect scrollRect;

    [Inject(Id = "level_select_button_prefab")] private GameObject levelSelectButtonPrefab;
    [Inject] private SelectLevelButton.Factory levelButtonFactory;
    [Inject] private LevelDatas levelDatas;

    public void Initialize() {
      foreach (var levelData in this.levelDatas.Levels) {
        var prefab = this.levelButtonFactory.Create(this.levelSelectButtonPrefab);
        prefab.transform.SetParent(this.buttonRoot);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        prefab.SetLevelData(levelData);
      }

      StartCoroutine(this.scrollPosReset());
    }

    private IEnumerator scrollPosReset() {
      yield return null;
      this.scrollRect.verticalNormalizedPosition = 1;
    }

  }
}
