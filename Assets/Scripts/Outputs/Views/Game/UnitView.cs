using System;
using System.Collections.Generic;
using Controllers.Game.Views;
using DG.Tweening;
using DG.Tweening.Core;
using Domain.Game.Units;
using Outputs.Views.Game.Unit;
using TMPro;
using UnityEngine;
using Zenject;

namespace Outputs.Views.Game {
  public class UnitView : MonoBehaviour, IUnitView, IInitializable {
    private static readonly int HeightPc = Shader.PropertyToID("_HeightPc");

    [Inject] private readonly UnitViewSettings viewSettings;
    [Inject(Id = "background")] private readonly SpriteRenderer backgroundSprite;
    [Inject(Id = "life_total")] private readonly SpriteRenderer lifeTotalSprite;
    [Inject(Id = "life_left")] private readonly SpriteRenderer lifeLeftSprite;
    [Inject(Id = "title")] private readonly TextMeshPro title;

    [Inject(Id = "damage_particle_prefab")] private GameObject damageParticlePrefab;
    [Inject] private DamageParticle.Factory damageParticleFactory;

    private MaterialPropertyBlock propBlock;
    private List<Sequence> curDamageSequences = new List<Sequence>();
    private Dictionary<SpriteRenderer, Color> origColorForSprites = new Dictionary<SpriteRenderer, Color>();

    void Awake() {
      this.propBlock = new MaterialPropertyBlock();
    }

    public void Initialize() {
      var spritesForDmgEffect = this.getSpritesForDamageEffect();

      foreach (var sprite in spritesForDmgEffect) {
        this.origColorForSprites.Add(sprite, sprite.color);
      }
    }

    public void SetTitle(string title) {
      this.title.text = title;
    }

    public void SetTitleColor(Color color) {
      this.title.color = color;
    }

    public void OnDamageDealt(int healthLeft, int dmg, float healthPercentage) {
      var spritesToRedden = this.getSpritesForDamageEffect();

      if (this.curDamageSequences.Count > 0) {
        this.curDamageSequences.ForEach(seq => DOTween.Kill(seq));
      }

      for (int i = 0; i < spritesToRedden.Length; ++i) {
        var sprite = spritesToRedden[i];
        var origColor = this.origColorForSprites[sprite];
        sprite.color = origColor;

        var sequence = DOTween.Sequence()
          .Append(sprite.DOColor(this.viewSettings.DamagedColor, 0.1f))
          .Append(sprite.DOColor(origColor, 0.2f));
        sequence.OnComplete(() => this.curDamageSequences.Remove(sequence));

        this.curDamageSequences.Add(sequence);
      }

      this.setLifeTotalProperty(healthPercentage);

      var prefab = this.damageParticleFactory.Create(this.damageParticlePrefab);
      prefab.transform.SetParent(this.transform);
      prefab.transform.position = this.transform.position;
      prefab.AnimateDamage(dmg);
    }

    public void OnDeath(Action onComplete) {
      this.setLifeTotalProperty(0);

      var sprites = new SpriteRenderer[] {this.backgroundSprite, this.lifeTotalSprite, this.lifeLeftSprite};

      var setComplete = false;
      foreach (var sprite in sprites) {
        var tweener = sprite.DOColor(this.viewSettings.DeathColor, 0.4f);

        if (!setComplete) {
          tweener.OnComplete(() => onComplete());
          setComplete = true;
        }
      }

      this.title.DOColor(this.viewSettings.DeathColor, 0.4f);
    }

    private void setLifeTotalProperty(float healthPercentage) {
      this.lifeLeftSprite.GetPropertyBlock(this.propBlock);
      this.propBlock.SetFloat(HeightPc, healthPercentage);
      this.lifeLeftSprite.SetPropertyBlock(this.propBlock);
    }

    private SpriteRenderer[] getSpritesForDamageEffect() {
      return new SpriteRenderer[] {this.lifeTotalSprite, this.lifeLeftSprite};
    }
  }
}
