using System;
using System.Collections.Generic;

using Domain.States;

namespace Domain.States {
  public class StateMachine {
    private IGameState curState;

    public bool Running { get; private set; }

    public StateMachine(IGameState startState) {
      this.curState = startState;
    }

    public virtual void Start() {
      this.Running = true;

      this.curState.OnEnter();
    }

    public virtual void Stop() {
      this.Running = false;

      this.curState.Dispose();
    }

    public bool TransitionIfPossible() {
      if (!this.Running) {
        throw new InvalidOperationException("GameStateTracker instructed to try transition before starting");
      }

      bool transitioned = false;
      if (this.curState.CanTransition()) {
        var nextState = this.curState.GetStateToTransition();
        this.curState.OnExit();

        this.curState = nextState;
        this.curState.OnEnter();

        transitioned = true;
      }

      return transitioned;
    }
  }
}