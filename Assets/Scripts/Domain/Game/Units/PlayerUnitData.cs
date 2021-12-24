using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Domain.Game.Units {
  public class PlayerUnitData : UnitData {
    public enum PlayerType {
      KingKiller,
      Speed,
      Strength,
      FastAttack,
      Defense,
    }

    public readonly PlayerType UnitType;

    private readonly PlayerEffectBaseValueData baseValueData;

    public List<PlayerType> PlayerEffects { get; private set; } = new List<PlayerType>();

    public PlayerUnitData(PlayerType playerType, UnitBaseData unitBaseData, PlayerEffectBaseValueData effectBaseValueData) : base(unitBaseData) {
      this.UnitType = playerType;
      this.baseValueData = effectBaseValueData;
    }

    public override float Speed {
      get {
        var count = this.PlayerEffects.Count(pe => pe == PlayerType.Speed);
        return this.unitBaseData.Speed + this.baseValueData.SpeedBonusBase * count;
      }
    }

    public override int Damage {
      get {
        var count = this.PlayerEffects.Count(pe => pe == PlayerType.Strength);
        return this.unitBaseData.Damage + this.baseValueData.DamageBonusBase * count;
      }
    }

    public override int Health => this.unitBaseData.Health;

    public override float AttackCooldown {
      get {
        var count = this.PlayerEffects.Count(pe => pe == PlayerType.FastAttack);
        return Mathf.Max(this.unitBaseData.AttackCooldown - this.baseValueData.AttackCooldownReductionBase * count, 0);
      }
    }

    public override int Defense {
      get {
        var count = this.PlayerEffects.Count(pe => pe == PlayerType.Defense);
        return this.unitBaseData.Defense + this.baseValueData.DefenseBonusBase * count;
      }
    }

    public void AddPlayerEffect(PlayerType playerType) {
      this.PlayerEffects.Add(playerType);
    }

    public void RemovePlayerEffect(PlayerType playerType) {
      this.PlayerEffects.Remove(playerType);
    }

    public static string GetTitleFromPlayerType(PlayerUnitData.PlayerType playerType) {
      string title = String.Empty;

      switch (playerType) {
        case PlayerUnitData.PlayerType.KingKiller:
          title = "殺";
          break;
        case PlayerUnitData.PlayerType.Speed:
          title = "速";
          break;
        case PlayerUnitData.PlayerType.Strength:
          title = "強";
          break;
        case PlayerUnitData.PlayerType.FastAttack:
          title = "連";
          break;
        case PlayerUnitData.PlayerType.Defense:
          title = "盾";
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
      }

      return title;
    }
  }
}