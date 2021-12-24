using Outputs.Views;
using Outputs.Views.Game;
using UnityEngine;
using Zenject;

namespace  Installers.Game {
  [CreateAssetMenu(fileName = "UnitViewScriptableObjectInstaller", menuName = "Installers/UnitViewScriptableObjectInstaller")]
  public class UnitViewScriptableObjectInstaller : ScriptableObjectInstaller<UnitViewScriptableObjectInstaller> {
    [SerializeField] private UnitViewSettings viewSettings;

    public override void InstallBindings() {
      this.Container.BindInstance(this.viewSettings).AsSingle();
    }
  }
}
