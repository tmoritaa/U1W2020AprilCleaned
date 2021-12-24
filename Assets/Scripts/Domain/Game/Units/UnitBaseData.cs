using System;
using UnityEngine;

namespace Domain.Game.Units {
  [Serializable]
  public struct UnitBaseData {
    [SerializeField] private float speed;
    public float Speed => this.speed;

    [SerializeField] private int damage;
    public int Damage => this.damage;

    [SerializeField] private int health;
    public int Health => this.health;

    [SerializeField] private float attackCooldown;
    public float AttackCooldown => this.attackCooldown;

    [SerializeField] private int defense;
    public int Defense => this.defense;

    [SerializeField] private bool isTrueDamage;
    public bool IsTrueDamage => this.isTrueDamage;
  }
}