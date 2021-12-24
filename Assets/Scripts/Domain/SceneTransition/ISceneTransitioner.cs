using System;
using System.Collections.Generic;

namespace Domain.SceneTransition {
  public interface ISceneTransitioner {
    void TransitionToScene(string id);
  }
}