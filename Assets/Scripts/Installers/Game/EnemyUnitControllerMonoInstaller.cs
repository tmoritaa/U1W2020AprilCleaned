using Domain.Game.Units;
using Outputs.Views.Game.Unit;
using UnityEngine;
using Zenject;

namespace Installers.Game {
  public class EnemyUnitControllerMonoInstaller : MonoInstaller {
    [SerializeField] private GameObject damageParticlePrefab;

    public override void InstallBindings() {
      this.Container.BindInterfacesAndSelfTo<EnemyUnitData>().AsSingle();
      this.Container.BindInstance<GameObject>(this.damageParticlePrefab).WithId("damage_particle_prefab").AsSingle();
      this.Container.BindFactory<UnityEngine.Object, DamageParticle, DamageParticle.Factory>().FromFactory<PrefabFactory<DamageParticle>>();
    }
  }
}
