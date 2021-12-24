using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Outputs.Views.Game.Unit {
  public class DamageParticle : MonoBehaviour {
    [SerializeField] private TextMeshPro text;

    public void AnimateDamage(int damage) {
      this.text.text = string.Format("-{0}", damage);

      this.transform.DOMoveY(this.transform.position.y + 1f, 0.4f).SetEase(Ease.OutQuad)
        .OnComplete(() => Destroy(this.gameObject));
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, DamageParticle> {
    }
  }
}
