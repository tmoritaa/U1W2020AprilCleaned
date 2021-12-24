using System;
using System.Collections.Generic;
using System.Threading;

using UniRx.Async;

using Domain.States;

namespace Infrastructure {
  public class UniTaskStateMachineTransitionPoller : IStateMachineTransitionPoller {
    private CancellationTokenSource cancelTokenSource;

    public void StartPolling(StateMachine stateMachine) {
      this.cancelTokenSource = new CancellationTokenSource();

      this.pollStateMachineForTransition(stateMachine, this.cancelTokenSource.Token).Forget();
    }

    public void StopPolling() {
      this.Dispose();
    }

    public void Dispose() {
      this.cancelTokenSource?.Cancel();
      this.cancelTokenSource?.Dispose();
      this.cancelTokenSource = null;
    }

    private async UniTaskVoid pollStateMachineForTransition(StateMachine stateMachine, CancellationToken token) {
      while (!token.IsCancellationRequested) {
        await UniTask.Yield();

        if (stateMachine.Running) {
          stateMachine.TransitionIfPossible();
        }
      }
    }
  }
}