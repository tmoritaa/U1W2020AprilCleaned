using Outputs.Views.Start;
using UnityEngine;
using Zenject;

namespace Installers.Start {
  public class StartMonoInstaller : MonoInstaller {
    [SerializeField] private GameObject levelSelectButtonPrefab;

    public override void InstallBindings() {
      this.Container.BindInstance<GameObject>(this.levelSelectButtonPrefab).WithId("level_select_button_prefab").AsCached();
      this.Container.BindFactory<UnityEngine.Object, SelectLevelButton, SelectLevelButton.Factory>().FromFactory<PrefabFactory<SelectLevelButton>>();
    }
  }
}
