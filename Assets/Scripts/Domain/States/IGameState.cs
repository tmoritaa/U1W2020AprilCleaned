using System;
using System.Collections.Generic;

namespace Domain.States {
  public interface IGameState : IDisposable {
    void OnEnter();
    void OnExit();

    bool CanTransition();

    IGameState GetStateToTransition();
  }
}