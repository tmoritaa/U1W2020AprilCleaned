using System.Collections.Generic;
using Domain;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Installers {
  [CreateAssetMenu(fileName = "LevelInfoScriptableObjectInstaller", menuName = "Installers/LevelInfoScriptableObjectInstaller")]
  public class LevelInfoScriptableObjectInstaller : ScriptableObjectInstaller<LevelInfoScriptableObjectInstaller> {
    [SerializeField] private LevelDatas levelDatas;

    public override void InstallBindings() {
      this.Container.BindInstance(this.levelDatas);

      var curSceneName = SceneManager.GetActiveScene().name;

      if (this.levelDatas.LevelInfoExists(curSceneName)) {
        var curLevelData = this.levelDatas.GetLevelInfo(curSceneName);
        this.Container.BindInstance<LevelData>(curLevelData).WithId("cur_level_info");

        this.Container.BindInstance<LevelData.GameTypes>(curLevelData.GameType);

        var nextLevelData = this.levelDatas.GetNextLevelInfo(curLevelData.LevelName);
        this.Container.BindInstance<LevelData>(nextLevelData).WithId("next_level_info");
      }
    }
  }
}
