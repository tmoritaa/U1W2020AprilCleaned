using Domain.Game.Units;
using UnityEngine;
using Zenject;

namespace Installers.Game {
  [CreateAssetMenu(fileName = "UnitControllerScriptableObjectInstaller", menuName = "Installers/UnitControllerScriptableObjectInstaller")]
  public class UnitControllerScriptableObjectInstaller : ScriptableObjectInstaller<UnitControllerScriptableObjectInstaller> {
    [SerializeField] private UnitBaseData unitBaseData;

    public override void InstallBindings() {
      this.Container.BindInstance<UnitBaseData>(this.unitBaseData);
    }
  }
}
