using Zenject;

using Domain.States;

namespace Domain {
  public class GameStarter<T> : IInitializable where T : SelfPollingStateMachine {
    private readonly T stateMachine;

    public GameStarter(T stateMachine)
      => (this.stateMachine) = (stateMachine);

    public void Initialize() {
      this.stateMachine.Start();
    }
  }
}