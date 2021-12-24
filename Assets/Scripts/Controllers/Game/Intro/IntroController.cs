using System;
using Domain.Game.Controllers;
using Zenject;

namespace Controllers.Game.Intro {
  public class IntroController : IIntroController {
    private readonly IIntroView introView;

    public IntroController(IIntroView introView)
      => (this.introView) = (introView);

    public void PlayIntro(string title, string description, Action onComplete) {
      this.introView.PlayIntro(title, description, onComplete);
    }
  }
}