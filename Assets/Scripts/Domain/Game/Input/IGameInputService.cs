using System;
using UnityEngine;

namespace Domain.Game.Input {
  public interface IGameInputService {
    IObservable<Vector2> OnRightClick { get; }

    void StartListening();
    void StopListening();
  }
}