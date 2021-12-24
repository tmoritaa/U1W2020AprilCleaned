using System.Collections;
using System.Collections.Generic;
using Domain;
using Domain.SceneTransition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Outputs.Views.Game {
  public class GameUIView : MonoBehaviour, IInitializable {
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button returnToHomeButton;

    [Inject(Id = "cur_level_info")] private LevelData curLevelData;
    [Inject] private ISceneTransitioner sceneTransitioner;

    public void Initialize() {
      bool objectiveExists = this.curLevelData.LevelObjectives.Count > 0;

      this.objectiveText.gameObject.SetActive(objectiveExists);
      if (objectiveExists) {
        string objective = string.Empty;

        foreach (var levelObjective in this.curLevelData.LevelObjectives) {
          objective += levelObjective + "\n";
        }

        this.objectiveText.text = objective;
      }

      this.restartLevelButton.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene(this.curLevelData.LevelName));
      this.returnToHomeButton.onClick.AddListener(() => this.sceneTransitioner.TransitionToScene("Start"));
    }
  }
}

