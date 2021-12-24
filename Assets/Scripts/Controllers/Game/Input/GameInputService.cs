using System;
using System.Collections.Generic;
using System.Threading;
using Controllers.Game.Units;
using Domain.Game.Events;
using Domain.Game.Input;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Controllers.Game.Input {
  public class GameInputService : IGameInputService {
    private readonly Camera camera;
    private readonly SignalBus signalBus;
    private readonly LayerMask playerUnitCollisionLayerMask = LayerMask.GetMask("PlayerUnit");

    private CancellationTokenSource cancellationTokenSource;

    private readonly Subject<Vector2> rightClickSubject = new Subject<Vector2>();

    public bool IsListening => this.cancellationTokenSource != null;

    public IObservable<Vector2> OnRightClick => this.rightClickSubject;

    private Vector2 startMouseDragPos;

    public GameInputService(Camera camera, SignalBus signalBus) {
      this.camera = camera;
      this.signalBus = signalBus;
    }

    public void StartListening() {
      this.cancellationTokenSource = new CancellationTokenSource();
      this.ListenForKeyPress(this.cancellationTokenSource.Token).Forget();
    }

    public void StopListening() {
      if (this.cancellationTokenSource != null) {
        this.cancellationTokenSource.Cancel();
        this.cancellationTokenSource = null;
      }
    }

    protected virtual void performListenAction() {
      var mousePos = this.getCurWorldMousePos();

      if (UnityEngine.Input.GetMouseButtonUp(1)) {
        this.rightClickSubject.OnNext(mousePos);
      }

      if (UnityEngine.Input.GetMouseButtonDown(0)) {
        this.startMouseDragPos = mousePos;
      }

      if (UnityEngine.Input.GetMouseButtonUp(0)) {
        var endMousePos = mousePos;

        var diff = endMousePos - this.startMouseDragPos;

        var units = new List<PlayerUnitController>();
        if (diff.sqrMagnitude < 0.01f) {
          var result = Physics2D.Raycast(endMousePos, Vector2.zero, 0, this.playerUnitCollisionLayerMask);

          if (result.collider != null) {
            var unitController = result.transform.GetComponent<PlayerUnitController>();
            units.Add(unitController);
          }
        } else {
          var origin = (this.startMouseDragPos + endMousePos) / 2f;
          var size = new Vector2(Mathf.Abs(diff.x), Mathf.Abs(diff.y));

          var results = Physics2D.BoxCastAll(origin, size, 0, Vector2.zero, 0, this.playerUnitCollisionLayerMask);

          foreach (var result in results) {
            var unitController = result.transform.GetComponent<PlayerUnitController>();
            units.Add(unitController);
          }
        }

        this.signalBus.Fire(new UnitsClickedEvent(units));
      }
    }

    private Vector2 getCurWorldMousePos() {
      return this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
    }

    private async UniTaskVoid ListenForKeyPress(CancellationToken cancellationToken) {
      while (!cancellationToken.IsCancellationRequested) {
        this.performListenAction();

        await UniTask.Yield();
      }
    }
  }
}