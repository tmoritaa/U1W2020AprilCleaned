using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Installers.Game {
  [CreateAssetMenu(fileName = "PlayerUnitEffectDataScriptableObjectInstaller", menuName = "Installers/PlayerUnitEffectDataScriptableObjectInstaller")]
  public class PlayerUnitEffectDataScriptableObjectInstaller : ScriptableObjectInstaller<PlayerUnitEffectDataScriptableObjectInstaller> {
    [SerializeField] private PlayerEffectBaseValueData valueData;

    public override void InstallBindings() {
      this.Container.BindInstance<PlayerEffectBaseValueData>(this.valueData);
    }
  }
}
