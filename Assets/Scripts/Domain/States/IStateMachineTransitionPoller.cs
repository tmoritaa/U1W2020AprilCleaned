using System;
using System.Collections.Generic;

namespace Domain.States {
  public interface IStateMachineTransitionPoller : IDisposable {
    void StartPolling(StateMachine stateMachine);
    void StopPolling();
  }
}