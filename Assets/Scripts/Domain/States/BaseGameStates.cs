using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.States {
  public abstract class BaseGameState : IGameState {
    private List<Transition> transitions = new List<Transition>();

    protected bool IsRunning { get; private set; } = false;

    public void OnEnter() {
      this.IsRunning = true;
      this.onEnter();
    }

    public void OnExit() {
      this.IsRunning = false;
      this.onExit();
    }

    public bool CanTransition() => this.transitions.Any(t => t.Condition());

    public IGameState GetStateToTransition() {
      var validTransitions = this.transitions.FindAll(t => t.Condition());

      if (validTransitions.Count != 1) {
        throw validTransitions.Count > 1 ?
          new InvalidOperationException("Multiple valid transitions exist") : new InvalidOperationException("No valid transitions exist");
      }

      return validTransitions.First().State;
    }

    public void AddTransition(Transition transition) => this.transitions.Add(transition);

    public virtual void Dispose() {}

    protected abstract void onEnter();

    protected abstract void onExit();
  }

  public struct Transition {
    public readonly IGameState State;
    public readonly Func<bool> Condition;

    public Transition(IGameState state, Func<bool> condition) => (this.State, this.Condition) = (state, condition);
  }
}