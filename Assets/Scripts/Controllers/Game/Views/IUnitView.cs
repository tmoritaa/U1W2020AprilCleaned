using System;
using Domain.Game.Units;
using UnityEngine;

namespace Controllers.Game.Views {
  public interface IUnitView {
    void OnDamageDealt(int healthLeft, int dmg, float healthPercentage);
    void OnDeath(Action onComplete);
    void SetTitleColor(Color color);

    void SetTitle(string text);
  }
}