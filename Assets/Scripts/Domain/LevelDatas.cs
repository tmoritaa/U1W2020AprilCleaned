using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Domain {
  [Serializable]
  public class LevelDatas {
    [SerializeField] private List<LevelData> levels;
    public List<LevelData> Levels => this.levels;

    public bool LevelInfoExists(string levelId) {
      return this.levels.Exists(lvl => lvl.LevelName == levelId);
    }

    public LevelData GetLevelInfo(string levelId) {
      return this.levels.Find(lvl => lvl.LevelName == levelId);
    }

    public LevelData GetNextLevelInfo(string curLevelId) {
      var curLevelIdx = this.levels.FindIndex(lvl => lvl.LevelName == curLevelId);
      return curLevelIdx < this.levels.Count - 1 ? this.levels[curLevelIdx + 1] : new LevelData();
    }
  }
}