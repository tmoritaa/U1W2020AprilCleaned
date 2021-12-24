using System;
using UnityEngine;

namespace Outputs.Views.Game {
  [Serializable]
  public struct UnitViewSettings {
    [SerializeField] private Color damagedColor;
    public Color DamagedColor => this.damagedColor;

    [SerializeField] private Color deathColor;
    public Color DeathColor => this.deathColor;
  }
}