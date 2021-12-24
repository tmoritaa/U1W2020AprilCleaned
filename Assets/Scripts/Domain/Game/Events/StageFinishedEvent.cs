namespace Domain.Game.Events {
  public struct StageFinishedEvent {
    public readonly bool StageWon;

    public StageFinishedEvent(bool stageWon) {
      this.StageWon = stageWon;
    }
  }
}