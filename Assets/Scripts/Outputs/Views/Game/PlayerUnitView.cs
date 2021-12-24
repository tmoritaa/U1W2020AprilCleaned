using Controllers.Game.Views;
using UnityEngine;
using Zenject;

namespace Outputs.Views.Game {
  public class PlayerUnitView : UnitView, IPlayerUnitView {
    [Inject(Id = "select_outline")] private readonly SpriteRenderer selectBoxOutline;

    public void SetUnitSelectOutline(bool b) {
      this.selectBoxOutline.gameObject.SetActive(b);
    }
  }
}