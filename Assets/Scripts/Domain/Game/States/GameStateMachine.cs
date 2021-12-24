using Domain.States;

namespace Domain.Game.States {
  public class GameStateMachine : SelfPollingStateMachine {
    public GameStateMachine(
      GameSetupState setupState,
      GameIntroState introState,
      GamePlayState playState,
      GameCompleteWonState completeWonState,
      GameCompleteLostState completeLostState,
      IStateMachineTransitionPoller transitionPoller) : base(setupState, transitionPoller) {
      setupState.AddTransition(new Transition(introState, () => true));
      introState.AddTransition(new Transition(playState, () => introState.Complete));
      playState.AddTransition(new Transition(completeWonState, () => playState.Complete && playState.HasWon));
      playState.AddTransition(new Transition(completeLostState, () => playState.Complete && !playState.HasWon));
    }
  }
}