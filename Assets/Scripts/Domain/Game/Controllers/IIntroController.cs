using System;

namespace Domain.Game.Controllers {
  public interface IIntroController {
    void PlayIntro(string title, string description, Action onComplete);
  }
}