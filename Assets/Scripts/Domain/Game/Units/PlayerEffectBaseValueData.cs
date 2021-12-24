using System;
using UnityEngine;

namespace Domain.Game.Units {
  [Serializable]
  public struct PlayerEffectBaseValueData {
    [SerializeField] private float speedBonusBase;
    public float SpeedBonusBase => this.speedBonusBase;

    [SerializeField] private int damageBonusBase;
    public int DamageBonusBase => this.damageBonusBase;

    [SerializeField] private float attackCooldownReductionBase;
    public float AttackCooldownReductionBase => this.attackCooldownReductionBase;

    [SerializeField] private int defenseBonusBase;
    public int DefenseBonusBase => this.defenseBonusBase;
  }
}