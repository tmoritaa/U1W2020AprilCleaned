using System.Collections;
using System.Collections.Generic;
using Controllers.Game.Views;
using Domain.Game.Units;
using TMPro;
using UnityEngine;

namespace Outputs.Views.Game.Unit {
  public class EffectRingView : MonoBehaviour, IEffectRingView {
    [SerializeField] private List<TextMeshPro> texts;

    public void UpdateEffectRing(List<PlayerUnitData.PlayerType> playerTypes) {
      for (int i = 0; i < this.texts.Count; ++i) {
        if (i < playerTypes.Count) {
          var text = this.texts[i];
          text.gameObject.SetActive(true);
          text.text = PlayerUnitData.GetTitleFromPlayerType(playerTypes[i]);
        } else {
          this.texts[i].gameObject.SetActive(false);
        }
      }
    }
  }
}
