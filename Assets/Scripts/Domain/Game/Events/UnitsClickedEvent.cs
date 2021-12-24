using System.Collections.Generic;
using Domain.Game.Units;

namespace Domain.Game.Events {
  public class UnitsClickedEvent {
    public readonly IEnumerable<IPlayerUnitController> ClickedUnits;

    public UnitsClickedEvent(IEnumerable<IPlayerUnitController> clickedUnits) {
      this.ClickedUnits = clickedUnits;
    }
  }
}