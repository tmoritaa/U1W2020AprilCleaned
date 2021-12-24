using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domain {
  [Serializable]
  public struct LevelData {
    public enum GameTypes {
      Annihilation,
      KingKiller,
    }

    [SerializeField] private string levelName;
    public string LevelName => this.levelName;

    [SerializeField] private GameTypes gameType;
    public GameTypes GameType => this.gameType;

    [SerializeField] private string levelTitle;
    public string LevelTitle => this.levelTitle;

    [SerializeField] private string levelDescription;
    public string LevelDescription => this.levelDescription;

    [SerializeField] private List<string> levelObjectives;
    public List<string> LevelObjectives => this.levelObjectives;

    public string GetPlayerPrefStr() {
      return this.LevelName + "_complete";
    }
  }
}