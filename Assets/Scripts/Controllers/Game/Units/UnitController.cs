using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Game.Views;
using Domain.Game.Events;
using Domain.Game.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace Controllers.Game.Units {
  public abstract class UnitController : MonoBehaviour, IUnitController, IInitializable, ITickable, IFixedTickable, IDisposable {
    [Inject] protected readonly IUnitView unitView;
    [Inject] protected readonly IUnitData unitData;
    [Inject] protected readonly SignalBus signalBus;

    protected Rigidbody2D rigidbody2D;

    private readonly List<IUnitController> unitsInAttackRange = new List<IUnitController>();
    private readonly Subject<Unit> deathSubject = new Subject<Unit>();

    private bool attackInCooldown = false;

    private bool isDead = false;

    private bool firstTimeDamageDealt = true;

    protected bool actAllowed { get; private set; } = false;

    public int Health { get; private set; }

    public IObservable<Unit> OnDeath => this.deathSubject;

    public Vector2 WorldPos => this.transform.position;

    public void Initialize() {
      this.rigidbody2D = this.GetComponent<Rigidbody2D>();
      this.Health = this.unitData.Health;

      this.signalBus.Subscribe<GameplayStartEvent>(this.onGameStart);
      this.signalBus.Subscribe<StageFinishedEvent>(this.onGameComplete);

      this.onInitialize();
    }

    public void Dispose() {
      this.signalBus.Unsubscribe<GameplayStartEvent>(this.onGameStart);
      this.signalBus.Unsubscribe<StageFinishedEvent>(this.onGameComplete);
      this.deathSubject?.Dispose();
    }

    public void OnUnitEnterAttackRange(IUnitController unit) {
      this.unitsInAttackRange.Add(unit);
    }

    public void OnUnitExitAttackRange(IUnitController unit) {
      this.unitsInAttackRange.Remove(unit);
    }

    public void DamageDealt(int dmg, bool isTrueDamage) {
      if (this.firstTimeDamageDealt) {
        this.signalBus.Fire<FirstTimeDamageDealtEvent>();
        this.firstTimeDamageDealt = false;
      }

      var defense = isTrueDamage ? 0 : this.unitData.Defense;

      var actualDmg = Math.Max(dmg - defense, 0);

      this.Health -= actualDmg;
      if (this.Health <= 0) {
        this.setForDeath();
        this.unitView.OnDeath(this.deathSubject.OnCompleted);
      } else {
        this.unitView.OnDamageDealt(this.Health, actualDmg, (float)this.Health / this.unitData.Health);
      }
    }

    public void Tick() {
      if (this.actAllowed && !this.isDead) {
        this.handleDamageDealing();
      }
    }

    public void FixedTick() {
      if (this.actAllowed && !this.isDead) {
        this.handleMovement();
      }
    }

    protected virtual void onInitialize() {
    }

    protected abstract void handleMovement();

    private void handleDamageDealing() {
      if (!this.attackInCooldown && this.unitsInAttackRange.Count > 0) {
        this.unitsInAttackRange.ForEach(unit => unit.DamageDealt(this.unitData.Damage, this.unitData.IsTrueDamage));
        this.attackInCooldown = true;
        this.StartCoroutine(this.countdownAttackCooldown());
      }
    }

    private void setForDeath() {
      this.isDead = true;
      this.rigidbody2D.velocity = Vector2.zero;
    }

    private IEnumerator countdownAttackCooldown() {
      yield return new WaitForSeconds(this.unitData.AttackCooldown);
      this.attackInCooldown = false;
    }

    private void onGameStart(GameplayStartEvent evt) {
      this.actAllowed = true;
    }

    private void onGameComplete(StageFinishedEvent evt) {
      this.rigidbody2D.velocity = Vector2.zero;
      this.actAllowed = false;
    }
  }
}