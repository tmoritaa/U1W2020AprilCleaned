namespace Domain.Game.Controllers {
  public interface IGameCompleteController {
    void Show(bool hasWon);
    void Hide();
  }
}