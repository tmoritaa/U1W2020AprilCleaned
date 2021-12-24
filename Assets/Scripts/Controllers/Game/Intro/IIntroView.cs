using System;

namespace Controllers.Game.Intro {
  public interface IIntroView {
    void PlayIntro(string title, string description, Action onComplete);
  }
}